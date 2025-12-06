using Refit;

namespace Woozle.API.Spotify.Identity.Api;

public interface ISpotifyIdentityApi
{
	[Post("/api/token")]
	[Headers(SpotifyConstants.UrlEncodedContentTypeHeader)]
	Task<IApiResponse<SpotifyAccessTokenResponseModel>> RequestAccessTokenAsync(
		[Header(Constants.Authorization)] string accessToken,
		[Body(BodySerializationMethod.UrlEncoded)] SpotifyAccessTokenRequestModel request,
		CancellationToken cancellationToken);

	[Post("/api/token")]
	[Headers(SpotifyConstants.UrlEncodedContentTypeHeader)]
	Task<IApiResponse<SpotifyAccessTokenResponseModel>> RefreshTokenAsync(
		[Header(Constants.Authorization)] string accessToken,
		[Body(BodySerializationMethod.UrlEncoded)] SpotifyRefreshTokenRequestModel request,
		CancellationToken cancellationToken);
}
