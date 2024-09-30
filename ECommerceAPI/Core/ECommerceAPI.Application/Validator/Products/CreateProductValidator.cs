using ECommerceAPI.Application.ViewModel.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Validator.Products
{
	public class CreateProductValidator:AbstractValidator<CreateProductVM>
	{
        public CreateProductValidator()
        {
            RuleFor(n => n.Name)
                .NotEmpty()
                .NotNull().WithMessage("Name can't be null")
                .MinimumLength(5)
                .MaximumLength(15)
                .WithMessage("Name can be minimum 5 and maximum 15");

            RuleFor(s => s.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Stock can't be null")
                .Must(x => x >= 0)
                .WithMessage("Stock can't be negative");

			RuleFor(s => s.Price)
				.NotEmpty()
				.NotNull()
				.WithMessage("Price can't be null")
				.Must(x => x >= 0)
				.WithMessage("Price can't be negative");

		}
    }
}
