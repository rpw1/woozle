using Woozle.API.Spotify.Identity.Api;

namespace Woozle.API.Spotify.Identity;

public interface ISpotifyClientIdentityService
{
	Task<SpotifyAccessTokenResponseModel?> RequestSpotifyAccessTokenAsync(SpotifyAccessTokenRequestModel request, CancellationToken cancellationToken);

	Task<SpotifyAccessTokenResponseModel?> RequestSpotifyAccessTokenAsync(SpotifyRefreshTokenRequestModel request, CancellationToken cancellationToken);
}
