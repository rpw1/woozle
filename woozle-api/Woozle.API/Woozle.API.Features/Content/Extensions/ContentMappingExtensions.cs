using Woozle.API.Features.Content.Models;
using Woozle.API.Spotify.Content.Models;

namespace Woozle.API.Features.Content.Extensions;

public static class ContentMappingExtensions
{
	public static ImageModel ToDto(this SpotifyImageModel model) =>
		new()
		{
			Url = model.Url,
			Height = model.Height,
			Width = model.Width
		};

	public static TrackModel ToDto(this SpotifySimplifiedTrackModel model, ImageModel image) =>
		new()
		{
			Id = model.Id,
			Name = model.Name,
			Artist = string.Join(", ", model.Artists.Select(x => x.Name)),
			TrackUri = model.Uri,
			Explicit = model.Explicit,
			Image = image
		};

	public static ContentModel ToDto(this SpotifySavedAlbumModel model) =>
		new()
		{
			Id = model.Album.Id,
			ContentType = ContentType.Album,
			Name = model.Album.Name,
			Image = model.Album.Images.First().ToDto()
		};

	public static ContentModel ToDto(this SpotifyArtistModel model) =>
		new()
		{
			Id = model.Id,
			ContentType = ContentType.Artist,
			Name = model.Name,
			Image = model.Images.First().ToDto()
		};

	public static ContentModel ToDto(this SpotifySimplifiedPlaylistModel model) =>
		new()
		{
			Id = model.Id,
			ContentType = ContentType.Playlist,
			Name = model.Name,
			Description = model.Description,
			Image = model.Images.First().ToDto()
		};

	public static TrackModel ToDto(this SpotifyPlaylistTrackModel model) =>
		new()
		{
			Id = model.Track.Id,
			Name = model.Track.Name,
			Artist = string.Join(", ", model.Track.Artists.Select(x => x.Name)),
			TrackUri = model.Track.Uri,
			Explicit = model.Track.Explicit,
			Image = model.Track.Album.Images.First().ToDto()
		};
};
