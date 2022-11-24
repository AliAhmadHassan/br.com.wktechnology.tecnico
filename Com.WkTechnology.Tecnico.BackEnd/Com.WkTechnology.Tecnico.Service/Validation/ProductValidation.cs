using Com.WkTechnology.Tecnico.Domain.DTO.Category;
using Com.WkTechnology.Tecnico.Domain.DTO.Product;
using FluentValidation;

namespace Com.WkTechnology.Tecnico.Service.Validation
{
    public class ProductValidation: AbstractValidator<ProductDTO>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("{0} is required");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("{0} is maximun length of 50");
            RuleFor(x => x.Description).MaximumLength(50).WithMessage("{0} is maximun lenght of 50");

            RuleFor(x => x.UnitPrice).NotEmpty().NotNull().WithMessage("{0} is required");
            RuleFor(x => x.Amount).NotEmpty().NotNull().WithMessage("{0} is required");
        }
    }
}
