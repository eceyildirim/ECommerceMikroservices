using FluentValidation;
using OrderService.Application.Models;

namespace OrderService.Application.Validators;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.AddressLine)
            .NotEmpty().WithMessage("Adres satırı boş olamaz.")
            .MaximumLength(200).WithMessage("Adres 200 karakteri geçemez.");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Posta kodu boş olamaz.")
            .Matches(@"^\d{5}$").WithMessage("Posta kodu 5 haneli olmalıdır.");

        RuleFor(x => x.Country)
            .NotNull().WithMessage("Ülke bilgisi gereklidir.");

        RuleFor(x => x.Province)
            .NotNull().WithMessage("İl bilgisi gereklidir.");

        RuleFor(x => x.District)
            .NotNull().WithMessage("İlçe bilgisi gereklidir.");

        RuleFor(x => x.Neighborhood)
            .NotNull().WithMessage("Mahalle bilgisi gereklidir.");

        RuleFor(x => x.Customer)
            .NotNull().WithMessage("Müşteri bilgisi gereklidir.")
            .SetValidator(new CustomerDtoValidator());
    }
}