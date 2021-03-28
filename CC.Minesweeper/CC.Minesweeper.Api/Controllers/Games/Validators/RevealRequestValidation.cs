using CC.Minesweeper.Api.Controllers.Games.Models;
using FluentValidation;

namespace CC.Minesweeper.Api.Controllers.Games.Validators
{
    /// <summary>
    /// The RevealRequest validator.
    /// </summary>
    public class RevealRequestValidation : AbstractValidator<RevealRequest>
    {
        public RevealRequestValidation()
        {
            RuleFor(x => x.Row)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Column)
                .GreaterThanOrEqualTo(0);
        }
    }
}
