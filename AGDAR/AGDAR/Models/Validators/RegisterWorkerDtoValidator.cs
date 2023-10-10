using AGDAR.Models.DTO;
using AGDAR.Repositories;
using FluentValidation;

namespace AGDAR.Models.Validators
{
    public class RegisterWorkerDtoValidator : AbstractValidator<WorkerDto>
    {
        public RegisterWorkerDtoValidator(WorkerRepository workerRepository) 
        { 
            RuleFor(x=>x.Email).NotEmpty().EmailAddress();
            RuleFor(x=>x.Password).MinimumLength(6);
            RuleFor(x=>x.ConfirmPassword).Equal(e=>e.Password);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var workers = workerRepository.GetAll();
                foreach (var worker in workers)
                {
                    if (worker.Email == value)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                }
            });
        }

    }
}
