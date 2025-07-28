using OrderService.Application.Models.Requests;
using FluentValidation;

namespace OrderService.Application.Validators;

public class UpdateOrderRequestModelValidator : AbstractValidator<UpdateOrderRequestModel>
{
    public UpdateOrderRequestModelValidator()
    {
        RuleFor(x => x.OrderId)
            .NotNull().WithMessage("Sipariş numarası gereklidir.");

        RuleFor(x => x.OrderStatus)
            .NotNull().WithMessage("Sipariş statüsü gereklidir.");
    }
}