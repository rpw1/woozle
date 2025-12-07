using Woozle.API.Features.Content.Models;
using Woozle.API.Spotify.Content.Models;

namespace Woozle.API.Features.Content;

public static class ContentMappers
{
	extension(SpotifyImageModel model)
	{
        public ImageModel ToDto()
			=> new()
			{
				Url = model.Url,
				Height = model.Height,
				Width = model.Width
			};
    }
	
	extension(SpotifySimplifiedTrackModel model)
	{
        public TrackModel ToDto(ImageModel image)
			=> new()
			{
				Id = model.Id,
				Name = model.Name,
				Artist = string.Join(", ", model.Artists.Select(x => x.Name)),
				TrackUri = model.Uri,
				Explicit = model.Explicit,
				Image = image
			};
    }
	
	extension(SpotifySavedAlbumModel model)
	{
        public ContentModel ToDto()
			=> new()
			{
				Id = model.Album.Id,
				ContentType = ContentType.Album,
				Name = model.Album.Name,
				Image = model.Album.Images.First().ToDto()
			};
    }
	
	extension(SpotifyArtistModel model)
	{
        public ContentModel ToDto()
			=> new()
			{
				Id = model.Id,
				ContentType = ContentType.Artist,
				Name = model.Name,
				Image = model.Images.First().ToDto()
			};
    }

	extension(SpotifySimplifiedPlaylistModel model)
	{
        public ContentModel ToDto()
			=> new()
			{
				Id = model.Id,
				ContentType = ContentType.Playlist,
				Name = model.Name,
				Description = model.Description,
				Image = model.Images.First().ToDto()
			};
    }
	
	extension(SpotifyPlaylistTrackModel model)
	{
        public TrackModel ToDto()
			=> new()
			{
				Id = model.Track.Id,
				Name = model.Track.Name,
				Artist = string.Join(", ", model.Track.Artists.Select(x => x.Name)),
				TrackUri = model.Track.Uri,
				Explicit = model.Track.Explicit,
				Image = model.Track.Album.Images.First().ToDto()
			};
    }
};
