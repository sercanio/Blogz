using NArchitecture.Core.Application.Responses;

namespace Application.Features.Entries.Commands.Update;

public class DeletePostResponse : IResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}