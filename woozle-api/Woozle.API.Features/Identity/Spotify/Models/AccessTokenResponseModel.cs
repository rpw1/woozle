
namespace Woozle.API.Features.Identity.Spotify.Models;

public sealed class AccessTokenResponseModel
{
	public required string AccessToken { get; set; }

	public required int ExpiresIn { get; set; }

	public string? RefreshToken { get; set; }

	public required string Scope { get; set; }

	public required string TokenType { get; set; }
}
