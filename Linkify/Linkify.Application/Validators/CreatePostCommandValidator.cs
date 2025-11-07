using FluentValidation;
using Linkify.Application.Features.Posts.Commands;

namespace Linkify.Application.Validators;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Post content is required.")
            .MaximumLength(1000).WithMessage("Post content cannot exceed 1000 characters.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be valid.");
    }
}