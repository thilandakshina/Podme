using FluentValidation;
using Podme.Application.Commands;

namespace Podme.Application.Validators
{
    public class PauseSubscriptionCommandValidator : AbstractValidator<PauseSubscriptionCommand>
    {
        public PauseSubscriptionCommandValidator()
        {
            RuleFor(x => x.SubscriptionId)
                .NotEmpty().WithMessage("Subscription ID is required");
        }
    }
}
