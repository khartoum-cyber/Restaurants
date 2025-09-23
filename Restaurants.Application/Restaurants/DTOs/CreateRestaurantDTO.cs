using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs
{
    public class CreateRestaurantDTO
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }
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
