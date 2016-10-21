using DatacircleAPI.Models;

namespace DatacircleAPI.Repositories
{
    public interface IConnectionDetailsRepository
    {
        ConnectionDetails Create(ConnectionDetails connectionDetails);
        ConnectionDetails Get(int connectionDetailsId);
        void Update(ConnectionDetails connectionDetails);
        void Delete(int connectionDetailsId);
        int Save();
    }
}
