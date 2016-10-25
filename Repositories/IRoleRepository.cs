using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;

namespace DatacircleAPI.Repositories
{
    public interface IRoleRepository
    {
        Role Create(Role role);
        Role Get(int roleId);
        void Update(Role role);
        void Delete(int roleId);
        int Save();
        Role getDefaultNewRole();
    }
}
