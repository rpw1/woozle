using System.Text.Json.Serialization;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content.Models;

public sealed class SpotifySimplifiedPlaylistModel : SpotifyBaseModel
{
	[JsonPropertyName("collaborative")]
	public bool Collaborative { get; set; }

	[JsonPropertyName("description")]
	public string? Description { get; set; }

	[JsonPropertyName("name")]
	public required string Name { get; set; }

	[JsonPropertyName("public")]
	public bool? Public { get; set; }

	[JsonPropertyName("images")]
	public required ICollection<SpotifyImageModel> Images { get; set; }
}
