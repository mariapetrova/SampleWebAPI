using Application.Common.Interfaces;
using Application.Validations;
using Domain.Entities;
using FluentValidation;

namespace Leads.Application.LeadsHandlers.Commands.UpdateLead;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator(IApplicationDbContext context)
    {
        RuleFor(l => l)
            .NotNull()
            .DependentRules(() =>
            {
                RuleFor(l => l.Id)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty()
                    .MustAsync(async (personId, cancellationToken) =>
                    {
                        var person = await context.Persons.FindAsync(
                            new object[] { personId, cancellationToken }, cancellationToken: cancellationToken);
                        return person != null;
                    }).WithMessage(ValidationConstants.PersonNotFound);

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

                RuleFor(l => l.DepartmentId)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .MustAsync(async (id, cancellationToken) =>
                    {
                        var department = await context.Departments.FindAsync(
                            new object[] { id, cancellationToken }, cancellationToken: cancellationToken);
                        return department != null;
                    }).WithMessage(ValidationConstants.DepartmentNotFound);
            });
    }
}