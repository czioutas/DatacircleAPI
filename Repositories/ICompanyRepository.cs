using DatacircleAPI.Models;

namespace DatacircleAPI.Repositories
{
    public interface ICompanyRepository
    {
        Company Create(Company company);
        Company Get(int company);
        Company GetByName(string companyName);
        void Update(Company company);
        void Delete(int company);
        int Save();
    }
}
