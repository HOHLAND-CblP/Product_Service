using FluentValidation;
using hohland_cblp.ShopBackend.Domain.Entities;


namespace hohland_cblp.ShopBackend.Domain.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(item => item.Id)
            .NotNull();
        RuleFor(item => item.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(20);
        RuleFor(item => item.Price)
            .NotNull();
        RuleFor(item => item.Currency)
            .Length(3)
            .NotNull();
        RuleFor(item => item.CreationDate)
            .NotNull();
        RuleFor(item => item.ProductType)
            .NotNull();
    }
}