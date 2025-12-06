using System.Text;

namespace Woozle.API.Spotify;

public sealed class SpotifySettings()
{
	public required string AccountsBaseUrl { get; set; }

	public required string ApiBaseUrl { get; set; }

	public string ClientCredentialsAuthorization => "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret));

	public required string ClientId { get; set; }

	public required string ClientSecret { get; set; }
};
