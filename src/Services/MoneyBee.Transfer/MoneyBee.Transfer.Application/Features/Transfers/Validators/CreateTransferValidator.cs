using FluentValidation;
using MoneyBee.Transfer.Application.Features.Transfers.Commands;

namespace MoneyBee.Transfer.Application.Features.Transfers.Validators;

public class CreateTransferValidator : AbstractValidator<CreateTransferCommand>
{
    public CreateTransferValidator()
    {
        RuleFor(x => x.SenderCustomerId).NotEmpty();
        RuleFor(x => x.ReceiverCustomerId).NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Transfer tutarı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(10000)
            .WithMessage("Tek seferde maksimum 10.000 TRY gönderilebilir.");

        RuleFor(x => x.Currency).IsInEnum();
    }
}