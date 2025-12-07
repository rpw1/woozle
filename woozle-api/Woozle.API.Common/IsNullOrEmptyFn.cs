namespace Woozle.API.Common;

public static class IsNullOrEmptyFn
{
	extension(string? input)
	{
        public bool IsNullOrEmpty()
        {
            return string.IsNullOrEmpty(input);
        }
    }
}
