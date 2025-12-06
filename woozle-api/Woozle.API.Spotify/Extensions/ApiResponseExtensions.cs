using Refit;
using System.Net;

namespace Woozle.API.Spotify.Extensions;

public static class ApiResponseExtensions
{
	public static void ValidateAndThrow(this IApiResponse response)
	{
		if (response.StatusCode == HttpStatusCode.Unauthorized)
		{
			throw new UnauthorizedAccessException();
		}
	}
}
