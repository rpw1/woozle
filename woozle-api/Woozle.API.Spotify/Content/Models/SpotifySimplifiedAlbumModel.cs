using System.Text.Json.Serialization;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content.Models;

public class SpotifySimplifiedAlbumModel : SpotifyBaseModel
{
	[JsonPropertyName("album_type")]
	public required string AlbumTypeSetter { private get; set; }

	public SpotifyAlbumType AlbumType => AlbumTypeSetter switch
	{
		"album" => SpotifyAlbumType.Album,
		"single" => SpotifyAlbumType.Single,
		"compilation" => SpotifyAlbumType.Compilation,
		_ => throw new InvalidOperationException($"Album type {AlbumTypeSetter} is currently not defined for SpotifyAlbumType.")
	};

	[JsonPropertyName("total_tracks")]
	public required int TotalTracks { get; set; }

	[JsonPropertyName("images")]
	public required ICollection<SpotifyImageModel> Images { get; set; }

	[JsonPropertyName("name")]
	public required string Name { get; set; }

	[JsonPropertyName("release_date")]
	public required string ReleaseDate { get; set; }

	[JsonPropertyName("release_date_precision")]
	public required string ReleaseDatePrecision { get; set; }

	[JsonPropertyName("artists")]
	public ICollection<SpotifySimplifiedArtistModel> Artists { get; set; } = [];
}
