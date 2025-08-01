using System;
using FluentValidation;
using StockService.Application.Validators;
using StockService.Application.Models.Requests;
namespace StockService.Application.Validators;

public class UpdateStockRequestModelValidator : AbstractValidator<UpdateStockRequestModel>
{
    public UpdateStockRequestModelValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEqual(Guid.Empty)
            .WithMessage("OrderId bilgisi gereklidir.");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Sipariş ürünleri boş olamaz.")
            .NotEmpty().WithMessage("Siparişin en az 1 ürünü olmalıdı");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId).NotEqual(Guid.Empty).WithMessage("ProductId bilgisi gereklidir.");
            item.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("En az 1 adet ürün eklenmiş olmalıdır.");
        });

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Sipariş ürünleri boş olamaz.")
            .NotEmpty().WithMessage("Siparişin en az 1 ürünü olmalıdır.");
    }
}