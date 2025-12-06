using System.Text.Json.Serialization;

namespace Woozle.API.Spotify.Models;

public class SpotifyBaseModel
{
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	[JsonPropertyName("type")]
	public required string Type { private get; set; }

	public SpotifyModelType ContentType => Type switch
	{
		"album" => SpotifyModelType.Album,
		"artist" => SpotifyModelType.Artist,
		"episode" => SpotifyModelType.Episode,
		"track" => SpotifyModelType.Track,
		"user" => SpotifyModelType.User,
		_ => throw new InvalidOperationException($"Content type of ${Type} is currently not defined in SpotifyContentType.")
	};

	[JsonPropertyName("href")]
	public required string Href { get; set; }

	[JsonPropertyName("uri")]
	public required string Uri { get; set; }
}
