using System.Text.Json.Serialization;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content.Models;

public class SpotifySimplifiedArtistModel : SpotifyBaseModel
{
	[JsonPropertyName("name")]
	public required string Name { get; set; }
}
