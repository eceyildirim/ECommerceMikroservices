using OrderService.Application.Models.Requests;

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