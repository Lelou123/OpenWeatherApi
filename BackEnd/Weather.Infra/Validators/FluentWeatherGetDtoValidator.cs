using FluentValidation;
using Weather.Domain.Dtos.Requests;

namespace Weather.Infra.Validators;

public class FluentWeatherGetDtoValidator : AbstractValidator<WeatherGetDto>
{
    public FluentWeatherGetDtoValidator() 
    {
        RuleFor(x => x.Units).IsInEnum().WithMessage("Units must be a valid value");

        RuleFor(x => x.Latitude).NotEmpty().WithMessage("Latitude must have a value");

        RuleFor(x => x.Longitude).NotEmpty().WithMessage("Longitude must have a value");
    }
}