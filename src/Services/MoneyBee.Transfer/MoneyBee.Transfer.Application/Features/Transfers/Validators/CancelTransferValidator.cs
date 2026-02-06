using FluentValidation;
using MoneyBee.Transfer.Application.Features.Transfers.Commands;

namespace MoneyBee.Transfer.Application.Features.Transfers.Validators;
public class CancelTransferValidator : AbstractValidator<CancelTransferCommand>
{
    public CancelTransferValidator()
    {
        RuleFor(x => x.TransactionCode)
            .NotEmpty().WithMessage("İptal edilecek işlem kodu boş olamaz.")
            .Length(10).WithMessage("İşlem kodu 10 hane olmalıdır.");
    }
}