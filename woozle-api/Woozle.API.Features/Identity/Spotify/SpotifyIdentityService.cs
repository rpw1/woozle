using Woozle.API.Features.Identity.Spotify.Models;
using Woozle.API.Spotify.Identity;
using Woozle.API.Spotify.Identity.Api;

namespace Woozle.API.Features.Identity.Spotify;

public interface ISpotifyIdentityService
{
    Task<AccessTokenResponseModel?> RequestAccessTokenAsync(AccessTokenRequestModel request, CancellationToken cancellationToken);

    Task<AccessTokenResponseModel?> RequestAccessTokenAsync(RefreshTokenRequestModel request, CancellationToken cancellationToken);
}

[ServiceRegistration(ServiceLifeTimeRegistrationType.Scoped)]
public sealed class SpotifyIdentityService(ISpotifyClientIdentityService spotifyClientIdentityService) : ISpotifyIdentityService
{
    public async Task<AccessTokenResponseModel?> RequestAccessTokenAsync(AccessTokenRequestModel request, CancellationToken cancellationToken)
	{
		var spotifyRequestModel = new SpotifyAccessTokenRequestModel()
		{
			Code = request.Code,
			RedirectUri = request.RedirectUri
		};

		var result = await spotifyClientIdentityService.RequestSpotifyAccessTokenAsync(spotifyRequestModel, cancellationToken);

		if (result is null)
		{
			return null;
		}

		return new AccessTokenResponseModel()
		{
			AccessToken = result.AccessToken,
			ExpiresIn = result.ExpiresIn,
			RefreshToken = result.RefreshToken,
			Scope = result.Scope,
			TokenType = result.TokenType
		};
	}

	public async Task<AccessTokenResponseModel?> RequestAccessTokenAsync(RefreshTokenRequestModel request, CancellationToken cancellationToken)
	{
		var spotifyRequestModel = new SpotifyRefreshTokenRequestModel()
		{
			RefreshToken = request.RefreshToken
		};

		var result = await spotifyClientIdentityService.RequestSpotifyAccessTokenAsync(spotifyRequestModel, cancellationToken);

		if (result is null)
		{
			return null;
		}

		return new AccessTokenResponseModel()
		{
			AccessToken = result.AccessToken,
			ExpiresIn = result.ExpiresIn,
			RefreshToken = result.RefreshToken,
			Scope = result.Scope,
			TokenType = result.TokenType
		};
	}
}
