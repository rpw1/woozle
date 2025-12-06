namespace Woozle.API.Features.Content.Models;

public sealed class TrackModel
{
	public required string Id { get; set; }

	public bool? Explicit { get; set; }

	public required string Name { get; set; }

	public required string Artist { get; set; }

	public required string TrackUri { get; set; }

	public required ImageModel Image { get; set; }
}
