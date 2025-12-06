using System.Text.Json.Serialization;

namespace Woozle.API.Spotify.Models;

public sealed class SpotifyCursorsModel
{
	[JsonPropertyName("after")]
	public string? After { get; set; }

	[JsonPropertyName("before")]
	public string? Before { get; set; }
}
