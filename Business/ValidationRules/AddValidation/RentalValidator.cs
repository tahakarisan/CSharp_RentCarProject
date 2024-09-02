using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.AddValidation
{
    public class RentalValidator : AbstractValidator<RentalInfo>
    {
        public RentalValidator()
        {
            RuleFor(r => r.CarId).NotEmpty();
            RuleFor(r => r.ReturnDate).LessThan(DateTime.Now);

        }

    }
}
