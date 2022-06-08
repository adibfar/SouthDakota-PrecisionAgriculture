using FluentValidation;
using PAS.Application.Features.Farms.Commands.AddEdit;

namespace PAS.Application.Validators.Features.Farms.Commands.AddEdit
{
    public class AddEditFarmCommandValidator : AbstractValidator<AddEditFarmCommand>
    {
        public AddEditFarmCommandValidator()
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "Name is required!");
            RuleFor(request => request.Description)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "Description is required!");
        }
    }
}