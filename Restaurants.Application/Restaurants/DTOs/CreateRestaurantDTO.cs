using System.ComponentModel.DataAnnotations;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs
{
    public class CreateRestaurantDTO
    {
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        [Required(ErrorMessage = "Insert a valid category")]
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public string? ContactEmail { get; set; }
        [Phone(ErrorMessage = "Please provide a valid phone number")]
        public string? ContactNumber { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Please provide a valid postal code (XX-XXX).")]
        public string? PostalCode { get; set; }

        public static Restaurant? FromEntity(CreateRestaurantDTO? restaurant)
        {
            if (restaurant == null)
                return null;

            return new Restaurant()
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                HasDelivery = restaurant.HasDelivery,
                ContactEmail = restaurant.ContactEmail,
                ContactNumber = restaurant.ContactNumber,
                Address = new Address()
                {
                    City = restaurant.City,
                    Street = restaurant.Street,
                    PostalCode = restaurant.PostalCode
                }
            };
        }
    }
}
