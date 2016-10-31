using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;

namespace DatacircleAPI.Repositories
{
    public interface IRoleRepository
    {
        Role Create(Role role);
        Role Get(int roleId);
        Role getDefaultNewRole();
        void Update(Company company);
        void Delete(int company);
        int Save();
    }
}
