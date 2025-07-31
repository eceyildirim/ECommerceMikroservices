using OrderService.Common;
using Microsoft.AspNetCore.Mvc;
using OrderService.Common.Models;
using OrderService.Common.Exceptions;
namespace OrderService.API.Middleware;

public static class CustomValidationResponseExtension
{
    ///<summary>
    /// FluentValidation kütüphanesinin hata mesajlarını özelleştirmek için kullanılmıştır.
    /// </summary>
    /// 
    public static void UseCustomValidationResponse(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                List<string> validationMessages = errors.Any() ? errors.ToList() : new List<string>();
                ValidationResponseModel errorDetails = new ValidationResponseModel
                {
                    IsSuccess = false,
                    Code = ErrorCodes.VALIDATION_ERROR_CODE,
                    Message = ErrorMessages.VALIDATION_ERROR_MESSAGE,
                    ValidationErrors = validationMessages
                };

                return new BadRequestObjectResult(errorDetails);
            };
        });
    }

}