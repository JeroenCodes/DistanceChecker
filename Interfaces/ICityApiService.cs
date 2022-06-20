using DistanceChecker.Models;

namespace DistanceChecker.Interfaces
{
    public interface ICityApiService
    {
        Task<IEnumerable<City>> GetCity(string name);
        Task<double> CalculateDistance(double latitude1, double longitude1, double latitude2, double longitude2);
    }
}
