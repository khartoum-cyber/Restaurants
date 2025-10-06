using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder : IRestaurantSeeder
    {
        private readonly RestaurantsDbContext _context;
        public RestaurantSeeder(RestaurantsDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Seed()
        {
            if (await _context.Database.CanConnectAsync())
            {
                if (!_context.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _context.Restaurants.AddRange(restaurants);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new(UserRoles.User),
                new(UserRoles.Owner),
                new(UserRoles.Admin),
            };
            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new()
            {
                new Restaurant
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Nashville Hot Chicken",
                            Description = "Nashville Hot Chicken (10 pcs.)",
                            Price = 10.30M
                        },
                        new Dish
                        {
                            Name = "Chicken Nuggets",
                            Description = "Chicken Nuggets (5 pcs.)",
                            Price = 5.30M
                        },
                    },
                    Address = new Address
                    {
                        City = "London",
                        Street = "Cork St 5",
                        PostalCode = "WC2N 5DU"
                    }
                },
                new Restaurant
                {
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description = "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    HasDelivery = true,
                    Address = new Address
                    {
                        City = "London",
                        Street = "Boots 193",
                        PostalCode = "W1F 8SR"
                    },
                }
            };

            return restaurants;
        }
    }
}