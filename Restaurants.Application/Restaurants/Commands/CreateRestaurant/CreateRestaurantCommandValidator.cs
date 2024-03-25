using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{

    private readonly List<string> validCategories = ["Turkish", "Italian", "Mexican", "Japanese", "American", "Indian"];

    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Category)
            //.Must(category => validCategories.Contains(category)) // lambda version
            .Must(validCategories.Contains) // or directly feed the incoming argument
            .WithMessage("Invalid category. Plese choose from the valid categories");
        //.Custom((value, context) =>
        //{
        //    var isvalidcategory = validCategories.Contains(value);
        //    if (!isvalidcategory)
        //    {
        //        context.AddFailure("Category", "Invalid category. Please choose from the valid categories");
        //    }
        //});

        /*
        * The rules below are redundant as ApiController already checks for non-nullable fields whether they are provided as filled or as empty.
        
        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Please provide a description");

        RuleFor(dto => dto.Category)
            .NotEmpty().WithMessage("Please provide a category");
        */

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code in the format XX-XXX");

    }
}
