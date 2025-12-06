using Refit;

namespace Woozle.API.Spotify.Models;
public sealed class SpotifyArtistAlbumsQueryParams : SpotifyLimitQueryParams
{
	[AliasAs("include_groups")]
	public string IncludeGroups { get; set; } = "album";

	public SpotifyArtistAlbumsQueryParams(int offset = 0) : base(offset)
	{

	}
}
