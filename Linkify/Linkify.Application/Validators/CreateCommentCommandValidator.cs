using FluentValidation;
using Linkify.Application.Features.Comments.Commands;

namespace Linkify.Application.Validators;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Comment content is required.")
            .MaximumLength(500).WithMessage("Comment content cannot exceed 500 characters.");

        RuleFor(x => x.PostId)
            .GreaterThan(0).WithMessage("Post ID must be valid.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be valid.");
    }
}