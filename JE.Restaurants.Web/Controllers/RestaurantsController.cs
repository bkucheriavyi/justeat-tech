using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JE.Restaurant.WebApi.Services.Interfaces;
using JE.Restaurant.WebApi.Dtos;

namespace JE.Restaurant.WebApi.Controllers
{
    [Route("restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly ILogger<RestaurantsController> _logger;
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(ILogger<RestaurantsController> logger, IRestaurantService restaurantService)
        {
            _logger = logger;
            _restaurantService = restaurantService;
        }

        [HttpGet("byPostCode/{postCode}")]
        [ProducesResponseType(typeof(RestaurantDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPostCode(string postCode)
        {
            var result = await _restaurantService.GetRestaurantsByPostCodeAsync(postCode);

            return Ok(result);
        }
    }
}
