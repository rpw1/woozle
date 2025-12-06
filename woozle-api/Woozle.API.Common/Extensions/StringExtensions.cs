namespace Woozle.API.Common.Extensions;

public static class StringExtensions
{
	extension(string? input)
	{
        public bool IsNullOrEmpty()
        {
            return string.IsNullOrEmpty(input);
        }
    }
}
