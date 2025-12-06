using System.Text.Json.Serialization;

namespace Woozle.API.Spotify.Identity.Api;
public sealed class SpotifyAccessTokenResponseModel
{
	[JsonPropertyName("access_token")]
	public required string AccessToken { get; set; }

	[JsonPropertyName("token_type")]
	public required string TokenType { get; set; }

	[JsonPropertyName("scope")]
	public required string Scope { get; set; }

	[JsonPropertyName("expires_in")]
	public required int ExpiresIn { get; set; }

	[JsonPropertyName("refresh_token")]
	public string? RefreshToken { get; set; }
}
