using Refit;
using Woozle.API.Spotify.Models;

namespace Woozle.API.Spotify.Content.Api;

public interface ISpotifyContentApi
{
	[Get("/artists/{id}/albums")]
	[Headers(SpotifyConstants.AuthorizationHeader)]
	Task<IApiResponse<SpotifyGetArtistAlbumsResponseModel>> GetArtistAlbumsAsync(
		[AliasAs("id")] string artistId,
		[Query] SpotifyArtistAlbumsQueryParams queryParams,
		CancellationToken cancellationToken);

	[Get("/albums/{id}")]
	[Headers(SpotifyConstants.AuthorizationHeader)]
	Task<IApiResponse<SpotifyGetAlbumResponseModel>> GetAlbumAsync(
		[AliasAs("id")] string albumId,
		CancellationToken cancellationToken);

	[Get("/albums/{id}/tracks")]
	[Headers(SpotifyConstants.AuthorizationHeader)]
	Task<IApiResponse<SpotifyGetAlbumTracksResponseModel>> GetAlbumTracksAsync(
		[AliasAs("id")] string albumId,
		[Query] SpotifyLimitQueryParams queryParams,
		CancellationToken cancellationToken);

	[Get("/me/albums")]
	[Headers(SpotifyConstants.AuthorizationHeader)]
	Task<IApiResponse<SpotifyGetSavedAlbumsResponseModel>> GetSavedAlbumsAsync(
		[Query] SpotifyLimitQueryParams queryParams,
		CancellationToken cancellationToken);

	[Get("/me/following")]
	[Headers(SpotifyConstants.AuthorizationHeader)]
	Task<IApiResponse<SpotifyGetFollowedArtistsResponseModel>> GetFollowedArtistsAsync(
		[Query] SpotifyGetFollowedArtistsQueryParams queryParams,
		CancellationToken cancellationToken);

	[Get("/me/playlists")]
	[Headers(SpotifyConstants.AuthorizationHeader)]
	Task<IApiResponse<SpotifyGetUserPlaylistsResponseModel>> GetUserPlaylistsAsync(
		[Query] SpotifyLimitQueryParams queryParams,
		CancellationToken cancellationToken);

	[Get("/playlists/{id}/tracks")]
	[Headers(SpotifyConstants.AuthorizationHeader)]
	Task<IApiResponse<SpotifyGetPlaylistTracksResponseModel>> GetPlaylistTracksAsync(
		[AliasAs("id")] string playlistId,
		[Query] SpotifyLimitQueryParams queryParams,
		CancellationToken cancellationToken);
}
