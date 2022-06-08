using FluentValidation;
using Microsoft.Extensions.Localization;
using PAS.Application.Features.Zones.Commands.AddEdit;

namespace PAS.Application.Validators.Features.Zones.Commands.AddEdit
{
    public class AddEditZoneCommandValidator : AbstractValidator<AddEditZoneCommand>
    {
        public AddEditZoneCommandValidator()
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "Name is required!");
            RuleFor(request => request.Description)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "Description is required!");
            RuleFor(request => request.FarmId)
                .GreaterThan(0).WithMessage(x => "Farm is required!");
        }
    }
}