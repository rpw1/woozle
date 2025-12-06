using System.Text.Json.Serialization;

namespace Woozle.API.Spotify.Content.Models;

public sealed class SpotifyImageModel
{
	[JsonPropertyName("url")]
	public required string Url { get; set; }

	[JsonPropertyName("height")]
	public int? Height { get; set; }

	[JsonPropertyName("width")]
	public int? Width { get; set; }
}
