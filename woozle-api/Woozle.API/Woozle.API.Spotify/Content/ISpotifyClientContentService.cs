using Woozle.API.Spotify.Content.Api;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content;

public interface ISpotifyClientContentService
{
	Task<SpotifyGetArtistAlbumsResponseModel?> GetSpotifyArtistsAlbumnsAsync(string artistId, CancellationToken cancellationToken, SpotifyArtistAlbumsQueryParams? queryParams = null);

	Task<SpotifyGetAlbumResponseModel?> GetSpotifyAlbumnAsync(string albumId, CancellationToken cancellationToken);

	Task<SpotifyGetAlbumTracksResponseModel?> GetSpotifyAlbumnTracksAsync(string albumId, CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null);

	Task<SpotifyGetSavedAlbumsResponseModel?> GetSpotifySavedAlbumsAsync(CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null);

	Task<SpotifyGetFollowedArtistsResponseModel?> GetSpotifyFollowedArtistsAsync(CancellationToken cancellationToken, SpotifyGetFollowedArtistsQueryParams? queryParams = null);

	Task<SpotifyGetUserPlaylistsResponseModel?> GetSpotifyUserPlaylistsAsync(CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null);

	Task<SpotifyGetPlaylistTracksResponseModel?> GetSpotifyPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null);
}
