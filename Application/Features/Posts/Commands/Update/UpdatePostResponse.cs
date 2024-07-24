using NArchitecture.Core.Application.Responses;

namespace Application.Features.Entries.Commands.Update;

public class UpdatePostResponse : IResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string Title { get; set; }
}