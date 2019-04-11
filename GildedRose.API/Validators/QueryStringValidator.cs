using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRose.ViewModels;
using FluentValidation;

namespace GildedRose.API.Validations
{
    public class QueryParamsValidator : AbstractValidator<QueryParams>
    {
        public QueryParamsValidator()
        {
            RuleFor(vm => vm.PageSize).NotEmpty().LessThan(0).WithMessage("PageSize cannot be negative");
        }
    }
}
