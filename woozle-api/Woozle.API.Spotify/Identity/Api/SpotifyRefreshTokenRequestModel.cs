using System.Text.Json.Serialization;

namespace Woozle.API.Spotify.Identity.Api;

public sealed class SpotifyRefreshTokenRequestModel
{
	[JsonPropertyName("grant_type")]
	public string GrantType { get; set; } = "refresh_token";

	[JsonPropertyName("refresh_token")]
	public required string RefreshToken { get; set; }
}
