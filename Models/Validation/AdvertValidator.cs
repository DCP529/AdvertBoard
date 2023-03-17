using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Validation
{
    public class AdvertValidator : AbstractValidator<Advert>
    {
        public AdvertValidator()
        {
            RuleFor(x => x.Number).NotNull().NotEmpty();
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.Rating).NotNull().NotEmpty().InclusiveBetween(0, 10);
        }
    }
}
