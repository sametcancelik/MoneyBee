using FluentValidation;
using MoneyBee.Transfer.Application.Features.Transfers.Commands;

namespace MoneyBee.Transfer.Application.Features.Transfers.Validators;
public class ReceiveMoneyValidator : AbstractValidator<ReceiveMoneyCommand>
{
    public ReceiveMoneyValidator()
    {
        RuleFor(x => x.TransactionCode)
            .NotEmpty().WithMessage("İşlem kodu boş olamaz.")
            .Matches(@"^MB[A-Z0-9]{8}$").WithMessage("Geçersiz işlem kodu formatı.");

        RuleFor(x => x.ReceiverCustomerId)
            .NotEmpty().WithMessage("Alıcı ID bilgisi gereklidir.");
    }
}