using Restaurants.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.Requests;

public class CreateRestaurantDto
{
    [StringLength(maximumLength: 100, MinimumLength = 3)]
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Please provide a valid category")]
    public string Category { get; set; } = default!;

    public bool HasDelivery { get; set; }

    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string? ContactEmail { get; set; }

    [Phone(ErrorMessage = "Please provide a valid phone number")]
    public string? ContactNumber { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Please provide a valid postal code in the format XX-XXX")]
    public string? PostalCode { get; set; }
}
