using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Woozle.API.Spotify.HttpMessageHandlers;

public sealed class SpotifyAuthorizationHandler : DelegatingHandler
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public SpotifyAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
	}

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var isTokenExtracted = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(Constants.Authorization, out var token);
		if (!isTokenExtracted || token.SingleOrDefault().IsNullOrEmpty())
		{
			throw new InvalidOperationException("Spotify authorization header must be applied to authorized endpoints");
		}

		request.Headers.Authorization = new AuthenticationHeaderValue(Constants.Bearer, token.Single());

		return base.SendAsync(request, cancellationToken);
	}
}
