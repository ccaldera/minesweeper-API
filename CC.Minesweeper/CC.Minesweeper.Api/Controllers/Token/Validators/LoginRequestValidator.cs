using CC.Minesweeper.Api.Controllers.Token.Models;
using FluentValidation;

namespace CC.Minesweeper.Api.Controllers.Token.Validators
{
    /// <summary>
    /// The LoginRequest validator.
    /// </summary>
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
