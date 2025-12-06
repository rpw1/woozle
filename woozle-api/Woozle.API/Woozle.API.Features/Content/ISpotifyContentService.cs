using Woozle.API.Features.Content.Models;

namespace Woozle.API.Features.Content;

public interface ISpotifyContentService
{
	Task<List<TrackModel>> GetArtistTracksAsync(string artistId, CancellationToken cancellationToken);

	Task<List<TrackModel>> GetAlbumnTracksAsync(string albumId, CancellationToken cancellationToken);

	Task<List<ContentModel>> GetSavedAlbumsAsync(CancellationToken cancellationToken);

	Task<List<ContentModel>> GetFollowedArtistsAsync(CancellationToken cancellationToken);

	Task<List<ContentModel>> GetUserPlaylistsAsync(CancellationToken cancellationToken);

	Task<List<TrackModel>> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken);
}
