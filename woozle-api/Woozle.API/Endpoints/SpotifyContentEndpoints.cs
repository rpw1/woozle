using Microsoft.AspNetCore.Mvc;
using Woozle.API.Endpoints.Filters;
using Woozle.API.Features.Content;
using Woozle.API.Features.Content.Models;

namespace Woozle.API.Endpoints;

public static class SpotifyContentEndpoints
{
    extension(RouteGroupBuilder routeBuilder)
    {
        public RouteGroupBuilder MapSpotifyContentEndpoints()
        {
            routeBuilder.AddEndpointFilter<AuthorizationHeaderFilter>()
                .AddEndpointFilter<ApiExceptionFilter>();

            routeBuilder.MapGet("/", GetContentAsync);

            routeBuilder.MapGet("/{id}/tracks", GetTracksAsync);

            return routeBuilder;
        }
    }

    public static async Task<IResult> GetContentAsync(ISpotifyContentService spotifyContentService, CancellationToken cancellationToken)
    {
        var albumns = spotifyContentService.GetSavedAlbumsAsync(cancellationToken);
        var artists = spotifyContentService.GetFollowedArtistsAsync(cancellationToken);
        var playlists = spotifyContentService.GetUserPlaylistsAsync(cancellationToken);

        List<ContentModel> contents = [];

        await foreach (var content in Task.WhenEach([albumns, artists, playlists]))
        {
            contents.AddRange(await content);
        }

        return TypedResults.Ok(contents);
    }

    public static async Task<IResult> GetTracksAsync(ISpotifyContentService spotifyContentService,
        string id,
        [FromQuery] ContentType contentType,
        CancellationToken cancellationToken
        )
    {
        var tracks = contentType switch
        {
            ContentType.Album => await spotifyContentService.GetAlbumnTracksAsync(id, cancellationToken),
            ContentType.Artist => await spotifyContentService.GetArtistTracksAsync(id, cancellationToken),
            ContentType.Playlist => await spotifyContentService.GetPlaylistTracksAsync(id, cancellationToken),
            _ => throw new NotImplementedException()
        };

        return TypedResults.Ok(tracks);
    }
}
