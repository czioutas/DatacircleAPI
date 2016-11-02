using DatacircleAPI.Models;

namespace DatacircleAPI.Repositories
{
    public interface IRoleRepository
    {
        Role Create(Role role);
        Role Get(int roleId);
        Role getDefaultNewRole();
        void Update(Role role);
        void Delete(int roleId);
        int Save();
    }
}
