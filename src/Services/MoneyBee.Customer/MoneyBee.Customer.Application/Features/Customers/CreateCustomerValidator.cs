using FluentValidation;
using MoneyBee.Customer.Application.Common;
using MoneyBee.Customer.Application.Features.Customers.Commands;
using MoneyBee.Shared.Enums;

namespace MoneyBee.Customer.Application.Features.Customers;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        
        RuleFor(x => x.NationalId)
            .NotEmpty()
            .Length(11)
            .Must(TcValidator.IsValid).WithMessage("Geçersiz T.C. Kimlik Numarası.");

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .Must(BeAtLeast18).WithMessage("Müşteri 18 yaşından küçük olamaz.");

        RuleFor(x => x.TaxNumber)
            .NotEmpty()
            .When(x => x.Type == CustomerType.Corporate)
            .WithMessage("Kurumsal müşteriler için Vergi Numarası zorunludur.");
    }

    private bool BeAtLeast18(DateTime birthDate)
    {
        return birthDate <= DateTime.Now.AddYears(-18);
    }
}