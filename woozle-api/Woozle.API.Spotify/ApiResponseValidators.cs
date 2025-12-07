using Refit;
using System.Net;

namespace Woozle.API.Spotify;

public static class ApiResponseValidators
{
	extension(IApiResponse response)
	{
        public void ValidateAndThrow()
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
