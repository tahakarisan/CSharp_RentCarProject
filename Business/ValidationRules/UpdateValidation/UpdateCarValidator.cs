using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.UpdateValidation
{
    public class UpdateCarValidator : AbstractValidator<Car>
    {
        public UpdateCarValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(c => c.Description).MinimumLength(5);
            RuleFor(c => c.ModelYear).NotEmpty();
            RuleFor(c => c.DailyPrice).NotEmpty();
        }
    }
}
