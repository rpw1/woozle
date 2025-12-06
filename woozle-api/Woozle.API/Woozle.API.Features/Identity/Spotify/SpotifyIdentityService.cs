using Woozle.API.Features.Identity.Spotify.Models;
using Woozle.API.Spotify.Identity;
using Woozle.API.Spotify.Identity.Api;

namespace Woozle.API.Features.Identity.Spotify;

[ServiceRegistration(ServiceLifeTimeRegistrationType.Scoped)]
public sealed class SpotifyIdentityService : ISpotifyIdentityService
{
	private readonly ISpotifyClientIdentityService _spotifyClientIdentityService;

	public SpotifyIdentityService(ISpotifyClientIdentityService spotifyClientIdentityService)
	{
		_spotifyClientIdentityService = spotifyClientIdentityService ?? throw new ArgumentNullException(nameof(spotifyClientIdentityService));
	}

	public async Task<AccessTokenResponseModel?> RequestAccessTokenAsync(AccessTokenRequestModel request, CancellationToken cancellationToken)
	{
		var spotifyRequestModel = new SpotifyAccessTokenRequestModel()
		{
			Code = request.Code,
			RedirectUri = request.RedirectUri
		};

		var result = await _spotifyClientIdentityService.RequestSpotifyAccessTokenAsync(spotifyRequestModel, cancellationToken);

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

		var result = await _spotifyClientIdentityService.RequestSpotifyAccessTokenAsync(spotifyRequestModel, cancellationToken);

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
