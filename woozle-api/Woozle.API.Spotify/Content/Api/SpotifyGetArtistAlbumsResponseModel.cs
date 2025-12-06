using System.Text.Json.Serialization;
using Woozle.API.Spotify.Content.Models;

namespace Woozle.API.Spotify.Content.Api;

public sealed class SpotifyGetArtistAlbumsResponseModel
{
	[JsonPropertyName("href")]
	public required string Href { get; set; }

	[JsonPropertyName("limit")]
	public required int Limit { get; set; }

	[JsonPropertyName("next")]
	public string? Next { get; set; }

	[JsonPropertyName("offset")]
	public required int Offset { get; set; }

	[JsonPropertyName("previous")]
	public string? Previous { get; set; }

	[JsonPropertyName("total")]
	public required int Total { get; set; }

	[JsonPropertyName("items")]
	public List<SpotifySimplifiedAlbumModel> Items { get; set; } = [];
}
