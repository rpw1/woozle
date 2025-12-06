using Woozle.API.Features.Identity.Spotify.Models;

namespace Woozle.API.Features.Identity.Spotify;

public interface ISpotifyIdentityService
{
	Task<AccessTokenResponseModel?> RequestAccessTokenAsync(AccessTokenRequestModel request, CancellationToken cancellationToken);

	Task<AccessTokenResponseModel?> RequestAccessTokenAsync(RefreshTokenRequestModel request, CancellationToken cancellationToken);
}
