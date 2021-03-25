using CC.Minesweeper.Api.Controllers.Games.Models;
using FluentValidation;

namespace CC.Minesweeper.Api.Controllers.Games.Validators
{
    public class NewGameRequestValidator : AbstractValidator<NewGameRequest>
    {
        public NewGameRequestValidator()
        {
            RuleFor(x => x.Rows)
                .GreaterThan(0)
                .WithMessage("Rows must be greater than 0.");

            RuleFor(x => x.Columns)
                .GreaterThan(0)
                .WithMessage("Columns must be greater than 0.");

            RuleFor(x => x.Mines)
                .GreaterThan(0)
                .WithMessage("Mines must be greater than 0.");

            RuleFor(x => x)
                .Must(x => x.Rows * x.Columns > x.Mines)
                .WithMessage("The amount of mines must be lower than the cells in the board.");
        }
    }
}
