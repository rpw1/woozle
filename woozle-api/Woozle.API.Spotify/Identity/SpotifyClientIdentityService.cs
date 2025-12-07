using Microsoft.Extensions.Options;
using Woozle.API.Spotify.Identity.Api;

namespace Woozle.API.Spotify.Identity;

public interface ISpotifyClientIdentityService
{
    Task<SpotifyAccessTokenResponseModel?> RequestSpotifyAccessTokenAsync(SpotifyAccessTokenRequestModel request, CancellationToken cancellationToken);

    Task<SpotifyAccessTokenResponseModel?> RequestSpotifyAccessTokenAsync(SpotifyRefreshTokenRequestModel request, CancellationToken cancellationToken);
}

[ServiceRegistration(ServiceLifeTimeRegistrationType.Scoped)]
public sealed class SpotifyClientIdentityService(ISpotifyIdentityApi spotifyIdentityApi,
    IOptions<SpotifySettings> spotifySettings) : ISpotifyClientIdentityService
{
    public async Task<SpotifyAccessTokenResponseModel?> RequestSpotifyAccessTokenAsync(SpotifyAccessTokenRequestModel request, CancellationToken cancellationToken)
	{
		var response = await spotifyIdentityApi.RequestAccessTokenAsync(spotifySettings.Value.ClientCredentialsAuthorization, request, cancellationToken);

		return response.Content;
	}

	public async Task<SpotifyAccessTokenResponseModel?> RequestSpotifyAccessTokenAsync(SpotifyRefreshTokenRequestModel request, CancellationToken cancellationToken)
	{
		var response = await spotifyIdentityApi.RefreshTokenAsync(spotifySettings.Value.ClientCredentialsAuthorization, request, cancellationToken);

		return response.Content;
	}
}
