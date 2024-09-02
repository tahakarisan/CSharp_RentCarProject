using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.UpdateValidation
{
    public class UpdateUserValidator : AbstractValidator<User>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.Id).NotEmpty();    
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.FirstName).MinimumLength(2);
            RuleFor(u => u.Password).MinimumLength(2);
            RuleFor(u => u.LastName).MinimumLength(2);

        }
    }
}
