using FluentValidation;
using StockService.Application.Validators;
using StockService.Application.Models.Requests;
namespace StockService.Application.Validators;

public class UpdateStockRequestModelValidator : AbstractValidator<UpdateStockRequestModel>
{
    public UpdateStockRequestModelValidator()
    {
        RuleFor(x => x.OrderId)
            .NotNull().WithMessage("OrderId bilgisi gereklidir.");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Sipariş ürünleri boş olamaz.")
            .NotEmpty().WithMessage("Siparişin en az 1 ürünü olmalıdır.");
    }
}