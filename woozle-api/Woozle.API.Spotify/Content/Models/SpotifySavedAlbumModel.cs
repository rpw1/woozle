using System.Text.Json.Serialization;

namespace Woozle.API.Spotify.Content.Models;

public sealed class SpotifySavedAlbumModel
{
	[JsonPropertyName("added_at")]
	public required string AddedAt { get; set; }

	[JsonPropertyName("album")]
	public required SpotifySimplifiedAlbumModel Album { get; set; }
}
