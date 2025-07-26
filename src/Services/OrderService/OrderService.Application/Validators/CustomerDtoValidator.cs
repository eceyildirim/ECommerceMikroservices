using FluentValidation;
using OrderService.Application.Models;

namespace OrderService.Application.Validators;

public class CustomerDtoValidator : AbstractValidator<CustomerDto>
{
    public CustomerDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş olamaz.")
            .MaximumLength(200).WithMessage("Ad 200 karakteri geçemez.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Soyad boş olamaz.")
            .MaximumLength(200).WithMessage("Soyad 200 karakteri geçemez.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi girilmelidir.")
            .MaximumLength(200).WithMessage("E-posta 200 karakteri geçemez.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
            .Matches(@"^\+90\s?5\d{2}\s?\d{3}\s?\d{4}$")
            .WithMessage("Geçerli bir telefon numarası girin.");
    }
}