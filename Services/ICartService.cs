namespace ErasmusProject.Services
{
    public interface ICartService
    {
        Task<double> CalculateCartTotalAsync(int userId);
        Task<long> CalculateTotalInCentsAsync(int userId);
    }
}