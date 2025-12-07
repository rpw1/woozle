namespace Woozle.API.Common;

public static class ExpandFn
{
	extension<T>(Func<int, Task<T?>> repeator)
	{
        public IAsyncEnumerable<Task<T?>> Expand(int total, int limit)
        {
            int totalRequests = (int)Math.Ceiling((decimal)(total - 1) / limit);

            var requests = Enumerable.Range(1, totalRequests)
                    .Select(offset => repeator(offset));

            return Task.WhenEach(requests);
        }
    }
}
