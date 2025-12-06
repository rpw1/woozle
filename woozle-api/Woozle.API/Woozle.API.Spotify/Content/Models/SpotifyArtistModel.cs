using System.Text.Json.Serialization;

namespace Woozle.API.Spotify.Content.Models;

public sealed class SpotifyArtistModel : SpotifySimplifiedArtistModel
{
	[JsonPropertyName("genres")]
	public ICollection<string> Genres { get; set; } = [];

	[JsonPropertyName("images")]
	public ICollection<SpotifyImageModel> Images { get; set; } = [];

	[JsonPropertyName("popularity")]
	public required int Popularity { get; set; }
}
