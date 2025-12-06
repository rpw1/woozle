namespace Woozle.API.Features.Identity.Spotify.Models;

public sealed class RefreshTokenRequestModel
{
	public required string RefreshToken { get; set; }
}
