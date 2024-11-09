using FluentValidation;
using Podme.Application.Commands;

namespace Podme.Application.Validators
{
    public class StartSubscriptionCommandValidator : AbstractValidator<StartSubscriptionCommand>
    {
        public StartSubscriptionCommandValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required");
        }
    }
}
