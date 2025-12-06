using System.Text.Json.Serialization;

namespace Woozle.API.Spotify.Identity.Api;

public sealed class SpotifyAccessTokenRequestModel
{
	[JsonPropertyName("grant_type")]
	public string GrantType { get; set; } = "authorization_code";

	[JsonPropertyName("code")]
	public required string Code { get; set; }

	[JsonPropertyName("redirect_uri")]
	public required string RedirectUri { get; set; }
}
