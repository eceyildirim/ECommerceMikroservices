using FluentValidation;
using OrderService.Application.Models.Requests;

namespace OrderService.Application.Validators;

public class OrderRequestModelValidator : AbstractValidator<OrderRequestModel>
{
    public OrderRequestModelValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEqual(Guid.Empty)
            .WithMessage("Müşteri bilgisi gereklidir.");

        RuleFor(x => x.Items).NotEmpty().WithMessage("Siparişin en az 1 ürünü olmalı.");
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId).NotEmpty();
            item.RuleFor(i => i.Quantity).GreaterThan(0);
            item.RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("Fiyat bilgisi sıfırdan büyük olmalı");
        });

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("Toplam tutar 0'dan büyük olmalıdır.");

        RuleFor(x => x.ShippingAddressId)
            .NotEqual(Guid.Empty)
            .WithMessage("Teslimat adresi boş olamaz.");

        RuleFor(x => x.TotalAmount)
            .NotEmpty().WithMessage("Toplam tutar boş olamaz.")
            .Must((model, totalAmount) =>
            {
                var calculatedAmount = model.Items.Sum(item => item.Quantity * item.UnitPrice);
                return totalAmount == calculatedAmount;
            })
            .WithMessage("Toplam tutar, ürünlerin toplam tutarıyla eşleşmiyor.");
    }
}