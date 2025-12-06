using Microsoft.AspNetCore.Mvc;
using Woozle.API.Features.Identity.Spotify;
using Woozle.API.Features.Identity.Spotify.Models;

namespace Woozle.API.Controllers;

[ApiController]
[Route("api/spotify/identity")]
public sealed class SpotifyIdentityController : ControllerBase
{
	private readonly ISpotifyIdentityService _spotifyIdentityService;

	public SpotifyIdentityController(ISpotifyIdentityService spotifyIdentityService)
	{
		_spotifyIdentityService = spotifyIdentityService ?? throw new ArgumentNullException(nameof(spotifyIdentityService));
	}

	[HttpPost("accessToken")]
	public async Task<IActionResult> RequestSpotifyAccessTokenAsync([FromBody] AccessTokenRequestModel request, CancellationToken cancellationToken)
	{
		return Ok(await _spotifyIdentityService.RequestAccessTokenAsync(request, cancellationToken));
	}

	[HttpPost("refreshToken")]
	public async Task<IActionResult> RefreshSpotifyAccessTokenAsync([FromBody] RefreshTokenRequestModel request, CancellationToken cancellationToken)
	{
		return Ok(await _spotifyIdentityService.RequestAccessTokenAsync(request, cancellationToken));
	}
}
