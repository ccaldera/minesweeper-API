using CC.Minesweeper.Api.Controllers.Users.Models;
using FluentValidation;

namespace CC.Minesweeper.Api.Controllers.Users.Validators
{
    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email is required")
                .EmailAddress()
                    .WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage("The password is required")
                .MinimumLength(6)
                    .WithMessage("Password must be al least 6 characters lenght");
        }
    }
}
