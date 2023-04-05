namespace WebApplication3.Services
{
    public interface IDbService
    {
        Task<T?> GetAsync<T>(string command, object parms);
        Task<List<T>> GetAll<T>(string command);
        Task<int> EditData(string command, object parms);
    }
}
