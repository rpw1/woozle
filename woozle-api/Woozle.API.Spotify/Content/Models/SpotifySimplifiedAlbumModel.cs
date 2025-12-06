using System.Text.Json.Serialization;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content.Models;

public class SpotifySimplifiedAlbumModel : SpotifyBaseModel
{
	// Not a big fan of this solution. I need a new class or to have a private field to change this.
	// Not crazy about either right now. Planning on cirlcing back later once I get a good idea of the flow of this work.
	[JsonPropertyName("album_type")]
	public required string AlbumTypeSetter { private get; set; }

	public SpotifyAlbumType AlbumType => AlbumTypeSetter switch
	{
		"album" => SpotifyAlbumType.Album,
		"single" => SpotifyAlbumType.Single,
		"compilation" => SpotifyAlbumType.Compilation,
		_ => throw new InvalidOperationException($"Albumn type {AlbumTypeSetter} is currently not defined for SpotifyAlbumType.")
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
