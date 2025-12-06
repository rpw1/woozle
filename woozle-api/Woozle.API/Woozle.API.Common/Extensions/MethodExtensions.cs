namespace Woozle.API.Common.Extensions;

public static class MethodExtensions
{
	public static IAsyncEnumerable<Task<T?>> Expand<T>(this Func<int, Task<T?>> repeator, int total, int limit)
	{
		int totalRequests = (int)Math.Ceiling((decimal)(total - 1) / limit);

		var requests = Enumerable.Range(1, totalRequests)
				.Select(offset => repeator(offset));

		return Task.WhenEach(requests);
	}
}
