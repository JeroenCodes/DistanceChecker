using DistanceChecker.Interfaces;
using DistanceChecker.Models;
using Microsoft.AspNetCore.Mvc;

namespace DistanceChecker.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    //[Authorize]
    public class CityApiController : ControllerBase
    {
        private readonly ICityApiService _cityApiService;

        public CityApiController(ICityApiService cityApiService)
        {
            _cityApiService = cityApiService;

        }

        //Get City - to test service
        [HttpGet("name")]
        public async Task<ActionResult<List<City>>> Get(string name)
        {
            IEnumerable<City> responseCities = await _cityApiService.GetCity(name);
            if (responseCities == null) return NotFound();
            return Ok(responseCities.ToList());
        }


        //Take two city names - calculate distance in a seperate service(?)
        [HttpGet("origin, destination")]
        public async Task<ActionResult<double>> GetDistance(string origin, string destination)
        {
            IEnumerable<City> originCities = await _cityApiService.GetCity(origin);
            if (originCities == null) return NotFound();
            IEnumerable<City> destinationCities = await _cityApiService.GetCity(destination);
            if (destinationCities == null) return NotFound();

            double distance = await _cityApiService.CalculateDistance(originCities.FirstOrDefault().Latitude, originCities.FirstOrDefault().Longitude, destinationCities.FirstOrDefault().Latitude, destinationCities.FirstOrDefault().Longitude);
            return Ok(distance);
        }
    }
}


