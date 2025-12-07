using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Woozle.API.Spotify.Authorization;

public sealed class SpotifyAuthorizationHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var isTokenExtracted = httpContextAccessor.HttpContext.Request.Headers.TryGetValue(Constants.Authorization, out var token);
		if (!isTokenExtracted || token.SingleOrDefault().IsNullOrEmpty())
		{
			throw new InvalidOperationException("Spotify authorization header must be applied to authorized endpoints");
		}

		request.Headers.Authorization = new AuthenticationHeaderValue(Constants.Bearer, token.Single());

		return base.SendAsync(request, cancellationToken);
	}
}
