// using FluentValidation;
// using Repositories.ViewModel.ProductViewModels;

// namespace ToyStore.Validator.Product;

// public class UpdateproductValidator : AbstractValidator<UpdateproductValidator>
// {
//     public UpdateproductValidator()
//     {
//         RuleFor(x => x.Brand)
//             .NotEmpty().WithMessage("Brand is required!");
//         RuleFor(x => x.Description)
//             .NotEmpty().WithMessage("Description is required!");
//         RuleFor(x => x.Categoryid)
//             .NotNull().WithMessage("Category id is required!");
//         RuleFor(x => x.Name)
//             .NotEmpty().WithMessage("Product's name is required!");
//         RuleFor(x => x.Price)
//             .NotEmpty().WithMessage("Price's name id is required!")
//             .GreaterThan(0).WithMessage("Price must be greater than zero.");
//         RuleFor(x => x.Img)
//             .NotEmpty().WithMessage("Product's image is required!");
//     }
// }