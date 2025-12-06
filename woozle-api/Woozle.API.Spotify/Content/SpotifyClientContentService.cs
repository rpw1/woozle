using Woozle.API.Spotify.Content.Api;
using Woozle.API.Spotify.Extensions;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content;

[ServiceRegistration(ServiceLifeTimeRegistrationType.Scoped)]
public sealed class SpotifyClientContentService : ISpotifyClientContentService
{
	private readonly ISpotifyContentApi _spotifyContentApi;

	public SpotifyClientContentService(ISpotifyContentApi spotifyContentApi)
	{
		_spotifyContentApi = spotifyContentApi ?? throw new ArgumentNullException(nameof(spotifyContentApi));
	}

	public async Task<SpotifyGetArtistAlbumsResponseModel?> GetSpotifyArtistsAlbumnsAsync(string artistId, CancellationToken cancellationToken, SpotifyArtistAlbumsQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await _spotifyContentApi.GetArtistAlbumsAsync(artistId, queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetAlbumResponseModel?> GetSpotifyAlbumnAsync(string albumId, CancellationToken cancellationToken)
	{
		var response = await _spotifyContentApi.GetAlbumAsync(albumId, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetAlbumTracksResponseModel?> GetSpotifyAlbumnTracksAsync(string albumId, CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await _spotifyContentApi.GetAlbumTracksAsync(albumId, queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetSavedAlbumsResponseModel?> GetSpotifySavedAlbumsAsync(CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await _spotifyContentApi.GetSavedAlbumsAsync(queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetFollowedArtistsResponseModel?> GetSpotifyFollowedArtistsAsync(CancellationToken cancellationToken, SpotifyGetFollowedArtistsQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await _spotifyContentApi.GetFollowedArtistsAsync(queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetUserPlaylistsResponseModel?> GetSpotifyUserPlaylistsAsync(CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await _spotifyContentApi.GetUserPlaylistsAsync(queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}

	public async Task<SpotifyGetPlaylistTracksResponseModel?> GetSpotifyPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken, SpotifyLimitQueryParams? queryParams = null)
	{
		queryParams ??= new();
		var response = await _spotifyContentApi.GetPlaylistTracksAsync(playlistId, queryParams, cancellationToken);

		response.ValidateAndThrow();
		return response.Content;
	}
}
