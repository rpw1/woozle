using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Woozle.API.Spotify.Content.Api;
using Woozle.API.Spotify.HttpMessageHandlers;
using Woozle.API.Spotify.Identity.Api;

namespace Woozle.API.Spotify;

public static class SpotifyServiceRegistration
{
	public static IServiceCollection RegisterSpotifyServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<SpotifySettings>(configuration.GetSection(nameof(SpotifySettings)));
		services.AddScoped<SpotifyAuthorizationHandler>();

		services.AddRefitClient<ISpotifyIdentityApi>()
			.ConfigureHttpClient(client => 
			{
				var spotifySettings = configuration.GetSection(nameof(SpotifySettings)).Get<SpotifySettings>();
				ArgumentException.ThrowIfNullOrEmpty(spotifySettings?.AccountsBaseUrl);

                client.BaseAddress = new Uri(spotifySettings.AccountsBaseUrl);
			});

		services.AddRefitClient<ISpotifyContentApi>()
			.ConfigureHttpClient(client =>
			{
				var spotifySettings = configuration.GetSection(nameof(SpotifySettings)).Get<SpotifySettings>();
				ArgumentException.ThrowIfNullOrEmpty(spotifySettings?.ApiBaseUrl);

				client.BaseAddress = new Uri(spotifySettings.ApiBaseUrl);
			})
			.AddHttpMessageHandler<SpotifyAuthorizationHandler>();

		services.RegisterDecoratedServices();

		return services;
	}
}
