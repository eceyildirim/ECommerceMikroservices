using FluentValidation;
using StockService.Application.Models.Requests;
namespace StockService.Application.Validators;

public class OrderItemRequestModelValidator : AbstractValidator<OrderItemRequestModel>
{
    public OrderItemRequestModelValidator()
    {
        RuleFor(x => x.ProductId)
        .NotNull().WithMessage("ProductId bilgisi gereklidir.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Ürün adeti 0'dan büyük olmalıdır.");
    }
}