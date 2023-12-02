using AGDAR.Models.DTO;
using FluentValidation;

namespace AGDAR.Models.Validators
{
    public class CreateEditCategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CreateEditCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nie może być puste");
        }
    }
}
