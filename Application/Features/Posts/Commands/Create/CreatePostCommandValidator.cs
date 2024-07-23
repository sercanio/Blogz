using FluentValidation;

namespace Application.Features.Posts.Commands.Create;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
    }
}