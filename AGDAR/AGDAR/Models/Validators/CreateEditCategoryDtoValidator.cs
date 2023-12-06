using AGDAR.Models.DTO;
using AGDAR.Repositories;
using FluentValidation;

namespace AGDAR.Models.Validators
{
    public class CreateEditCategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CreateEditCategoryDtoValidator()
        {
            RuleFor(x => x.Name).MinimumLength(1).WithMessage("To pole nie może być puste");
        }
    }
}
