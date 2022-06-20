using DistanceChecker.Interfaces;
using DistanceChecker.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DistanceChecker.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class CityApiController : ControllerBase
    {
        private readonly ICityApiService _cityApiService;

        public CityApiController(ICityApiService cityApiService)
        {
            _cityApiService = cityApiService;

        }

        /// <summary>
        /// Return the distance in Km between two cities
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        [HttpGet("CalculateDistance")]
        public async Task<ActionResult<double>> GetDistance([Required]string origin, [Required]string destination)
        {
            IEnumerable<City> originCities = await _cityApiService.GetCity(origin);
            if (originCities.Count() == 0) return NotFound($"{origin} not found");
            IEnumerable<City> destinationCities = await _cityApiService.GetCity(destination);
            if (destinationCities.Count() == 0) return NotFound($"{destination} not found");

            double distance = await _cityApiService.CalculateDistance(originCities.FirstOrDefault().Latitude, originCities.FirstOrDefault().Longitude, destinationCities.FirstOrDefault().Latitude, destinationCities.FirstOrDefault().Longitude);
            return Ok(distance);
        }
    }
}


