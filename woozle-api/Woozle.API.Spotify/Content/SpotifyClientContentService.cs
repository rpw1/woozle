using Woozle.API.Spotify.Content.Api;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content;

public interface ISpotifyClientContentService
{
    Task<SpotifyGetArtistAlbumsResponseModel?> GetSpotifyArtistsAlbumsAsync(string artistId, CancellationToken cancellationToken, SpotifyArtistAlbumsQueryParams? queryParams = null);

    Task<SpotifyGetAlbumResponseModel?> GetSpotifyAlbumAsync(string albumId, CancellationToken cancellationToken);

    Task<SpotifyGetAlbumTracksResponseModel?> GetSpotifyAlbumTracksAsync(string albumId, CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null);

    Task<SpotifyGetSavedAlbumsResponseModel?> GetSpotifySavedAlbumsAsync(CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null);

    Task<SpotifyGetFollowedArtistsResponseModel?> GetSpotifyFollowedArtistsAsync(CancellationToken cancellationToken, SpotifyGetFollowedArtistsQueryParams? queryParams = null);

    Task<SpotifyGetUserPlaylistsResponseModel?> GetSpotifyUserPlaylistsAsync(CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null);

    Task<SpotifyGetPlaylistTracksResponseModel?> GetSpotifyPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null);
}

[ServiceRegistration(ServiceLifeTimeRegistrationType.Scoped)]
public sealed class SpotifyClientContentService(ISpotifyContentApi spotifyContentApi) : ISpotifyClientContentService
{
    public async Task<SpotifyGetArtistAlbumsResponseModel?> GetSpotifyArtistsAlbumsAsync(string artistId, CancellationToken cancellationToken, SpotifyArtistAlbumsQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await spotifyContentApi.GetArtistAlbumsAsync(artistId, queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetAlbumResponseModel?> GetSpotifyAlbumAsync(string albumId, CancellationToken cancellationToken)
	{
		var response = await spotifyContentApi.GetAlbumAsync(albumId, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetAlbumTracksResponseModel?> GetSpotifyAlbumTracksAsync(string albumId, CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await spotifyContentApi.GetAlbumTracksAsync(albumId, queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetSavedAlbumsResponseModel?> GetSpotifySavedAlbumsAsync(CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await spotifyContentApi.GetSavedAlbumsAsync(queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetFollowedArtistsResponseModel?> GetSpotifyFollowedArtistsAsync(CancellationToken cancellationToken, SpotifyGetFollowedArtistsQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await spotifyContentApi.GetFollowedArtistsAsync(queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetUserPlaylistsResponseModel?> GetSpotifyUserPlaylistsAsync(CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await spotifyContentApi.GetUserPlaylistsAsync(queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetPlaylistTracksResponseModel?> GetSpotifyPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await spotifyContentApi.GetPlaylistTracksAsync(playlistId, queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}
}
