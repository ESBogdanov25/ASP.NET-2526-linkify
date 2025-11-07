using FluentValidation;
using Linkify.Application.Features.Comments.Commands;

namespace Linkify.Application.Validators;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .GreaterThan(0).WithMessage("Comment ID must be valid.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Comment content is required.")
            .MaximumLength(500).WithMessage("Comment content cannot exceed 500 characters.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be valid.");
    }
}