using FluentValidation;

namespace Application.Features.Authors.Commands.Create;

public class CreatePostCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreatePostCommandValidator()
    {
    }
}