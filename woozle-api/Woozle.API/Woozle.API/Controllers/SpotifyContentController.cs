using Microsoft.AspNetCore.Mvc;
using Woozle.API.Attributes;
using Woozle.API.Features.Content;
using Woozle.API.Features.Content.Models;

namespace Woozle.API.Controllers;

[ApiController]
[Route("api/spotify/content")]
public sealed class SpotifyContentController : ControllerBase
{
	private readonly ISpotifyContentService _spotifyContentService;

	public SpotifyContentController(ISpotifyContentService spotifyContentService)
	{
		_spotifyContentService = spotifyContentService ?? throw new ArgumentNullException(nameof(spotifyContentService));
	}

	[ApiExceptionFilter]
	[Authorization]
	[HttpGet]
	public async Task<IActionResult> GetContentAsync(CancellationToken cancellationToken)
	{
		var albumns = _spotifyContentService.GetSavedAlbumsAsync(cancellationToken);
		var artists = _spotifyContentService.GetFollowedArtistsAsync(cancellationToken);
		var playlists = _spotifyContentService.GetUserPlaylistsAsync(cancellationToken);

		List<ContentModel> contents = [];

		await foreach (var content in Task.WhenEach([albumns, artists, playlists]))
		{
			contents.AddRange(await content);
		}

		return Ok(contents);
	}

	[ApiExceptionFilter]
	[Authorization]
	[HttpGet("{id}/tracks")]
	public async Task<IActionResult> GetTracksAsync(string id, [FromQuery] ContentType contentType,  CancellationToken cancellationToken)
	{
		var tracks = contentType switch
		{
			ContentType.Album => await _spotifyContentService.GetAlbumnTracksAsync(id, cancellationToken),
			ContentType.Artist => await _spotifyContentService.GetArtistTracksAsync(id, cancellationToken),
			ContentType.Playlist => await _spotifyContentService.GetPlaylistTracksAsync(id, cancellationToken),
			_ => throw new NotImplementedException()
		};

		return Ok(tracks);
	}
}
