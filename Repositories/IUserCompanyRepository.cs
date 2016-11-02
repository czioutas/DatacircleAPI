using DatacircleAPI.Models;

namespace DatacircleAPI.Repositories
{
    public interface IUserRepository
    {
        User FindByVerificationToken(string verificationToken);  

        int Save();      
    }
}
