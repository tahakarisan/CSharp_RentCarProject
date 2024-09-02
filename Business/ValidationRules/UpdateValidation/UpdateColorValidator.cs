using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.UpdateValidation
{
    public class UpdateColorValidator : AbstractValidator<Color>
    {
        public UpdateColorValidator()
        {
            RuleFor(u => u.ColorId).NotEmpty();
            RuleFor(c => c.ColorName).NotEmpty().MinimumLength(2);
        }
    }
}
