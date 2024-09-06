using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.UpdateValidation
{
    public class UpdateBrandValidator : AbstractValidator<Brand>
    {
        public UpdateBrandValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(b => b.BrandName).MinimumLength(3);
        }
    }
}
