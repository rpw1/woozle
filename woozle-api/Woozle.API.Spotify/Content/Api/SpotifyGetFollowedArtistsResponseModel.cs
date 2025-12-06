using System.Text.Json.Serialization;
using Woozle.API.Spotify.Content.Models;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content.Api;

public sealed class SpotifyGetFollowedArtistsResponseModel
{
	[JsonPropertyName("artists")]
	public required SpotifyGetFollowedArtistsModel Artists { get; set; }
}

public sealed class SpotifyGetFollowedArtistsModel
{
	[JsonPropertyName("href")]
	public required string Href { get; set; }

	[JsonPropertyName("limit")]
	public required int Limit { get; set; }

	[JsonPropertyName("next")]
	public string? Next { get; set; }

	[JsonPropertyName("cursors")]
	public required SpotifyCursorsModel Cursors { get; set; }

	[JsonPropertyName("total")]
	public required int Total { get; set; }

	[JsonPropertyName("items")]
	public List<SpotifyArtistModel> Items { get; set; } = [];
}
