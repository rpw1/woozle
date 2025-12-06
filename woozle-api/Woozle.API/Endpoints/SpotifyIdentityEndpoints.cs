using Microsoft.AspNetCore.Mvc;
using Woozle.API.Features.Identity.Spotify;
using Woozle.API.Features.Identity.Spotify.Models;

namespace Woozle.API.Endpoints;

public static class SpotifyIdentityEndpoints
{
    extension(RouteGroupBuilder routeBuilder)
    {
        public RouteGroupBuilder MapSpotifyIdentityEndpoints()
        {
            routeBuilder.MapPost("/accessToken", RequestSpotifyAccessTokenAsync);

            routeBuilder.MapPost("/refreshToken", RefreshSpotifyAccessTokenAsync);

            return routeBuilder;
        }
    }

    public static async Task<IResult> RequestSpotifyAccessTokenAsync(ISpotifyIdentityService spotifyIdentityService, [FromBody] AccessTokenRequestModel request, CancellationToken cancellationToken)
        => TypedResults.Ok(await spotifyIdentityService.RequestAccessTokenAsync(request, cancellationToken));

    public static async Task<IResult> RefreshSpotifyAccessTokenAsync(ISpotifyIdentityService spotifyIdentityService, [FromBody] RefreshTokenRequestModel request, CancellationToken cancellationToken)
        => TypedResults.Ok(await spotifyIdentityService.RequestAccessTokenAsync(request, cancellationToken));
}
