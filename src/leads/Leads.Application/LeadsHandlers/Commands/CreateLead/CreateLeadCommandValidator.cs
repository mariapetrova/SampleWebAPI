using Application.Common.Interfaces;
using Application.Validations;
using FluentValidation;

namespace Leads.Application.LeadsHandlers.Commands.CreateLead;

public class CreateLeadCommandValidator : AbstractValidator<CreateLeadCommand>
{
    public CreateLeadCommandValidator(IApplicationDbContext context)
    {
        RuleFor(l => l.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .MaximumLength(ValidationConstants.MaximumNameLength);

        RuleFor(l => l.Address)
           .Cascade(CascadeMode.Stop)
           .NotNull()
           .NotEmpty()
           .MaximumLength(ValidationConstants.MinimumAddressLength);

        When(l => l.MobileNumber is not null || !string.IsNullOrWhiteSpace(l.MobileNumber), () =>
        {
            RuleFor(x => x.MobileNumber)
            .MaximumLength(ValidationConstants.MaximumMobileNumberLength)
            .DependentRules(() =>
            {
                RuleFor(x => x.MobileNumber)
                .Matches(ValidationConstants.MobileNumberRegex);
            });
        });

        When(l => l.EmailAddress is not null || !string.IsNullOrWhiteSpace(l.EmailAddress), () =>
        {
            RuleFor(x => x.EmailAddress)
            .MaximumLength(ValidationConstants.MaximumEmailLength)
            .DependentRules(() =>
            {
                RuleFor(x => x.EmailAddress)
                .Matches(ValidationConstants.EmailAddressRegex);
            });
        });

        RuleFor(l => l.PINCode)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .MaximumLength(ValidationConstants.MaximumPINCodeLength)
            .MustAsync(async (pinCode, cancellationToken) =>
            {
                var subarea = await context.Subareas.FindAsync(
                    new object[] { pinCode, cancellationToken }, cancellationToken: cancellationToken);
                return subarea != null;
            }).WithMessage(ValidationConstants.SubareaNotFound);
    }
}