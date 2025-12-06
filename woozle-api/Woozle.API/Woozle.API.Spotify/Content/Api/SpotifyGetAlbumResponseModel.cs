using System.Text.Json.Serialization;
using Woozle.API.Spotify.Content.Models;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content.Api;

public sealed class SpotifyGetAlbumResponseModel : SpotifyBaseModel
{
	[JsonPropertyName("images")]
	public ICollection<SpotifyImageModel> Images { get; set; } = [];
}
