using Microsoft.Extensions.Options;
using Woozle.API.Spotify.Identity.Api;

namespace Woozle.API.Spotify.Identity;

[ServiceRegistration(ServiceLifeTimeRegistrationType.Scoped)]
public sealed class SpotifyClientIdentityService : ISpotifyClientIdentityService
{
	private readonly ISpotifyIdentityApi _spotifyIdentityApi;
	private readonly SpotifySettings _spotifySettings;

	public SpotifyClientIdentityService(ISpotifyIdentityApi spotifyIdentityApi, 
		IOptions<SpotifySettings> spotifySettings)
	{
		_spotifyIdentityApi = spotifyIdentityApi ?? throw new ArgumentNullException(nameof(spotifyIdentityApi));
		_spotifySettings = spotifySettings.Value ?? throw new ArgumentNullException(nameof(spotifySettings));
	}

	public async Task<SpotifyAccessTokenResponseModel?> RequestSpotifyAccessTokenAsync(SpotifyAccessTokenRequestModel request, CancellationToken cancellationToken)
	{
		var response = await _spotifyIdentityApi.RequestAccessTokenAsync(_spotifySettings.ClientCredentialsAuthorization, request, cancellationToken);

		return response.Content;
	}

	public async Task<SpotifyAccessTokenResponseModel?> RequestSpotifyAccessTokenAsync(SpotifyRefreshTokenRequestModel request, CancellationToken cancellationToken)
	{
		var response = await _spotifyIdentityApi.RefreshTokenAsync(_spotifySettings.ClientCredentialsAuthorization, request, cancellationToken);

		return response.Content;
	}
}
