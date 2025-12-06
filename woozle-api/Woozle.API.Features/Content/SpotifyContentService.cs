using Woozle.API.Features.Content.Extensions;
using Woozle.API.Features.Content.Models;
using Woozle.API.Spotify;
using Woozle.API.Spotify.Content;
using Woozle.API.Spotify.Content.Api;

namespace Woozle.API.Features.Content;

[ServiceRegistration(ServiceLifeTimeRegistrationType.Scoped)]
public sealed class SpotifyContentService : ISpotifyContentService
{
	private readonly ISpotifyClientContentService _spotifyClientContentService;

	public SpotifyContentService(ISpotifyClientContentService spotifyClientContentService)
	{
		_spotifyClientContentService = spotifyClientContentService ?? throw new ArgumentNullException(nameof(spotifyClientContentService));
	}

	public async Task<List<TrackModel>> GetArtistTracksAsync(string artistId, CancellationToken cancellationToken)
	{
		var response = await _spotifyClientContentService.GetSpotifyArtistsAlbumnsAsync(artistId, cancellationToken);

		if (response is null)
		{
			return [];
		}

		List<string> albumIds = [..response.Items.Select(x => x.Id)];
		var expandFunction = (int offset) => _spotifyClientContentService.GetSpotifyArtistsAlbumnsAsync(artistId, cancellationToken, new(offset));

		await foreach (var request in expandFunction.Expand(response.Total, SpotifyConstants.ApiResultLimit))
		{
			var result = (await request)
				?.Items
				.Select(albums => albums.Id)
				?? [];

			albumIds.AddRange(result);
		}

		List<TrackModel> tracks = [];
		await foreach (var request in Task.WhenEach(albumIds.Select(id => GetAlbumnTracksAsync(id, cancellationToken))))
		{
			tracks.AddRange(await request);
		}

		return tracks;
	}

	public async Task<List<TrackModel>> GetAlbumnTracksAsync(string albumId, CancellationToken cancellationToken)
	{
		var album = await _spotifyClientContentService.GetSpotifyAlbumnAsync(albumId, cancellationToken);
		if (album is null)
		{
			return [];
		}

		var albumImage = album.Images.First().ToDto();

		var response = await _spotifyClientContentService.GetSpotifyAlbumnTracksAsync(albumId, cancellationToken);

		if (response is null)
		{
			return [];
		}

		List<TrackModel> tracks = [..response.Items.Select(track => track.ToDto(albumImage))];

		var expandFunction = (int offset) => _spotifyClientContentService.GetSpotifyAlbumnTracksAsync(albumId, cancellationToken, new(offset));

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
		var response = await _spotifyClientContentService.GetSpotifySavedAlbumsAsync(cancellationToken);

		if (response is null)
		{
			return [];
		}

		List<ContentModel> albums = [.. response.Items.Select(album => album.ToDto())];

		var expandFunction = (int offset) => _spotifyClientContentService.GetSpotifySavedAlbumsAsync(cancellationToken, new(offset));

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
			response = await _spotifyClientContentService.GetSpotifyFollowedArtistsAsync(cancellationToken, queryParams);

			artists.AddRange(response?.Artists.Items.Select(artist => artist.ToDto()) ?? []);

			queryParams.After = response?.Artists.Cursors.After;
		}
		while (response?.Artists.Next is not null);

		return artists;
	}

	public async Task<List<ContentModel>> GetUserPlaylistsAsync(CancellationToken cancellationToken)
	{
		var response = await _spotifyClientContentService.GetSpotifyUserPlaylistsAsync(cancellationToken);

		if (response is null)
		{
			return [];
		}

		List<ContentModel> playlists = [.. response.Items.Select(playlist => playlist.ToDto())];

		var expandFunction = (int offset) => _spotifyClientContentService.GetSpotifyUserPlaylistsAsync(cancellationToken, new(offset));

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
		var response = await _spotifyClientContentService.GetSpotifyPlaylistTracksAsync(playlistId, cancellationToken);

		if (response is null)
		{
			return [];
		}

		List<TrackModel> tracks = [.. response.Items.Where(item => item.Track is not null).Select(track => track.ToDto())];

		var expandFunction = (int offset) => _spotifyClientContentService.GetSpotifyPlaylistTracksAsync(playlistId, cancellationToken, new(offset));

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
