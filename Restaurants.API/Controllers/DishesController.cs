using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishes;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishForRestaurant;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/dishes")]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            var dishId = await _mediator.Send(command);
            return CreatedAtAction("GetByIdForRestaurant", new { restaurantId, dishId}, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetAllForRestaurant(int restaurantId)
        {
            var dishes = await _mediator.Send(new GetDishesForRestaurantQuery(restaurantId));

            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDTO?>> GetByIdForRestaurant(int restaurantId, int dishId)
        {
            var dish = await _mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));

            return Ok(dish);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDishesForRestaurant(int restaurantId)
        {
            await _mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }
    }
}
