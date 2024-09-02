using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.UpdateValidation
{
    public class UpdateBrandValidator : AbstractValidator<Brand>
    {
        public UpdateBrandValidator()
        {
            RuleFor(u => u.BrandId).NotEmpty();
            RuleFor(b => b.BrandName).MinimumLength(3);
        }
    }
}
