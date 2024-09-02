using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.UpdateValidation
{
    public class UpdateCustomerValidator : AbstractValidator<Customer>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(c => c.CompanyName).NotEmpty();
        }
    }
}
