using Refit;

namespace Woozle.API.Spotify.Models;

public class SpotifyLimitQueryParams
{
	public SpotifyLimitQueryParams(int offset = 0)
	{
		Offset = offset * Limit;
	}

	[AliasAs("limit")]
	public int Limit { get; set; } = SpotifyConstants.ApiResultLimit;

	[AliasAs("offset")]
	public int Offset { get; set; }
}
