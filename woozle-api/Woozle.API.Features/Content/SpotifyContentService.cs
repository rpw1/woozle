using Woozle.API.Features.Content.Models;
using Woozle.API.Spotify;
using Woozle.API.Spotify.Content;
using Woozle.API.Spotify.Content.Api;

namespace Woozle.API.Features.Content;

public interface ISpotifyContentService
{
    Task<List<TrackModel>> GetArtistTracksAsync(string artistId, CancellationToken cancellationToken);

    Task<List<TrackModel>> GetAlbumTracksAsync(string albumId, CancellationToken cancellationToken);

    Task<List<ContentModel>> GetSavedAlbumsAsync(CancellationToken cancellationToken);

    Task<List<ContentModel>> GetFollowedArtistsAsync(CancellationToken cancellationToken);

    Task<List<ContentModel>> GetUserPlaylistsAsync(CancellationToken cancellationToken);

    Task<List<TrackModel>> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken);
}

[ServiceRegistration(ServiceLifeTimeRegistrationType.Scoped)]
public sealed class SpotifyContentService(ISpotifyClientContentService spotifyClientContentService) : ISpotifyContentService
{
    public async Task<List<TrackModel>> GetArtistTracksAsync(string artistId, CancellationToken cancellationToken)
	{
		var response = await spotifyClientContentService.GetSpotifyArtistsAlbumsAsync(artistId, cancellationToken);

		if (response is null)
		{
			return [];
		}

		List<string> albumIds = [..response.Items.Select(x => x.Id)];
		var expandFunction = (int offset) => spotifyClientContentService.GetSpotifyArtistsAlbumsAsync(artistId, cancellationToken, new(offset));

		await foreach (var request in expandFunction.Expand(response.Total, SpotifyConstants.ApiResultLimit))
		{
			var result = (await request)
				?.Items
				.Select(albums => albums.Id)
				?? [];

			albumIds.AddRange(result);
		}

		List<TrackModel> tracks = [];
		await foreach (var request in Task.WhenEach(albumIds.Select(id => GetAlbumTracksAsync(id, cancellationToken))))
		{
			tracks.AddRange(await request);
		}

		return tracks;
	}

	public async Task<List<TrackModel>> GetAlbumTracksAsync(string albumId, CancellationToken cancellationToken)
	{
		var album = await spotifyClientContentService.GetSpotifyAlbumAsync(albumId, cancellationToken);
		if (album is null)
		{
			return [];
		}

		var albumImage = album.Images.First().ToDto();

		var response = await spotifyClientContentService.GetSpotifyAlbumTracksAsync(albumId, cancellationToken);

		if (response is null)
		{
			return [];
		}

		List<TrackModel> tracks = [..response.Items.Select(track => track.ToDto(albumImage))];

		var expandFunction = (int offset) => spotifyClientContentService.GetSpotifyAlbumTracksAsync(albumId, cancellationToken, new(offset));

		await foreach (var request in expandFunction.Expand(response.Total, SpotifyConstants.ApiResultLimit))
		{
			var result = (await request)
				?.Items
				.Where(track => track.IsPlayable)
				.Select(track => track.ToDto(albumImage))
				?? [];

			tracks.AddRange(result);
		}

		return tracks;
	}

	public async Task<List<ContentModel>> GetSavedAlbumsAsync(CancellationToken cancellationToken)
	{
		var response = await spotifyClientContentService.GetSpotifySavedAlbumsAsync(cancellationToken);

		if (response is null)
		{
			return [];
		}

		List<ContentModel> albums = [.. response.Items.Select(album => album.ToDto())];

		var expandFunction = (int offset) => spotifyClientContentService.GetSpotifySavedAlbumsAsync(cancellationToken, new(offset));

		await foreach (var request in expandFunction.Expand(response.Total, SpotifyConstants.ApiResultLimit))
		{
			var result = (await request)
				?.Items
				.Select(album => album.ToDto())
				?? [];

			albums.AddRange(result);
		}

		return albums;
	}

	public async Task<List<ContentModel>> GetFollowedArtistsAsync(CancellationToken cancellationToken)
	{
		SpotifyGetFollowedArtistsResponseModel? response;
		SpotifyGetFollowedArtistsQueryParams queryParams = new();
		List<ContentModel> artists = [];

		do
		{
			response = await spotifyClientContentService.GetSpotifyFollowedArtistsAsync(cancellationToken, queryParams);

			artists.AddRange(response?.Artists.Items.Select(artist => artist.ToDto()) ?? []);

			queryParams.After = response?.Artists.Cursors.After;
		}
		while (response?.Artists.Next is not null);

		return artists;
	}

	public async Task<List<ContentModel>> GetUserPlaylistsAsync(CancellationToken cancellationToken)
	{
		var response = await spotifyClientContentService.GetSpotifyUserPlaylistsAsync(cancellationToken);

		if (response is null)
		{
			return [];
		}

		List<ContentModel> playlists = [.. response.Items.Select(playlist => playlist.ToDto())];

		var expandFunction = (int offset) => spotifyClientContentService.GetSpotifyUserPlaylistsAsync(cancellationToken, new(offset));

		await foreach (var request in expandFunction.Expand(response.Total, SpotifyConstants.ApiResultLimit))
		{
			var result = (await request)
				?.Items
				.Select(playlist => playlist.ToDto())
				?? [];

			playlists.AddRange(result);
		}

		return playlists;
	}

	public async Task<List<TrackModel>> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken)
	{
		var response = await spotifyClientContentService.GetSpotifyPlaylistTracksAsync(playlistId, cancellationToken);

		if (response is null)
		{
			return [];
		}

		List<TrackModel> tracks = [.. response.Items.Where(item => item.Track is not null).Select(track => track.ToDto())];

		var expandFunction = (int offset) => spotifyClientContentService.GetSpotifyPlaylistTracksAsync(playlistId, cancellationToken, new(offset));

		await foreach (var request in expandFunction.Expand(response.Total, SpotifyConstants.ApiResultLimit))
		{
			var result = (await request)
				?.Items
				.Where(item => item.Track is not null
					&& item.Track.IsPlayable)
				.Select(track => track.ToDto())
				?? [];

			tracks.AddRange(result);
		}

		return tracks;
	}
}
