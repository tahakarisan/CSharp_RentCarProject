using Entities.Concrete;
using FluentValidation;
using System;

namespace Business.ValidationRules.UpdateValidation
{
    public class UpdateRentalValidator : AbstractValidator<RentalInfo>
    {
        public UpdateRentalValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(r => r.CarId).NotEmpty();
            RuleFor(r => r.ReturnDate).LessThan(DateTime.Now);

        }
    }
}
