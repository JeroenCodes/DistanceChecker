using DistanceChecker.Configuration;
using DistanceChecker.Interfaces;
using DistanceChecker.Models;
using Geolocation;
using Microsoft.Extensions.Options;

namespace DistanceChecker.Services
{
    public class CityApiService : ICityApiService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpResponseService _httpResponseService;
        private readonly CityApiConfiguration _options;


        public CityApiService(HttpClient httpClient, IOptions<CityApiConfiguration> options, HttpResponseService httpResponseService)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _httpResponseService = httpResponseService;
        }


        //Get city
        public async Task<IEnumerable<City>> GetCity(string name)
        {
            try
            {
                using var _request = new HttpRequestMessage(HttpMethod.Get, $"?name={name}");
                using var response = await _httpClient.SendAsync(_request, HttpCompletionOption.ResponseHeadersRead);
                if (response.IsSuccessStatusCode)
                {
                    var respObj = await _httpResponseService.DeserializeJsonFromStream<List<City>>(response);
                    if (respObj == null) return null;
                    return respObj;
                }
                var content = await _httpResponseService.StreamToStringAsync(await response.Content.ReadAsStreamAsync());

                throw new ApiException(message: content)
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Calculate distance
        public async Task<double> CalculateDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            Coordinate origin = new Coordinate(latitude1, longitude1);
            Coordinate destination = new Coordinate(latitude2, longitude2);
            DistanceUnit unit = DistanceUnit.Kilometers;
            double distance = GeoCalculator.GetDistance(origin, destination,2, unit);
            return distance;
        }
    }
}
