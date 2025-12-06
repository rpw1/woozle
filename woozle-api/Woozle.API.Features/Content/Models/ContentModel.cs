namespace Woozle.API.Features.Content.Models;

public sealed class ContentModel
{
	public required string Id { get; set; }

	public required ContentType ContentType { get; set; }

	public required string Name { get; set; }

	public string? Description { get; set; }

	public required ImageModel Image { get; set; }
}
