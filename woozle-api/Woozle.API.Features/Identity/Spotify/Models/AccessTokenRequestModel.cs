namespace Woozle.API.Features.Identity.Spotify.Models;

public class AccessTokenRequestModel
{
	public required string Code { get; set; }

	public required string RedirectUri { get; set; }
}
