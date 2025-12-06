using System.Text.Json.Serialization;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content.Models;

public sealed class SpotifyPlaylistTrackModel
{
	[JsonPropertyName("added_at")]
	public required string AddedAt { get; set; }

	[JsonPropertyName("added_by")]
	public SpotifyBaseModel? AddedBy { get; set; }

	//track = track or episode. Really could use a union type right now
	[JsonPropertyName("track")]
	public required SpotifyTrackModel Track { get; set; }
}
