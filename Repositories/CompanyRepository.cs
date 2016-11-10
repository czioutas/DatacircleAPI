using System;
using System.Linq;
using DatacircleAPI.Database;
using DatacircleAPI.Models;

namespace DatacircleAPI.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        Company ICompanyRepository.Create(Company company)
        {
            DateTime now = DateTime.Now;

            company.CreatedAt = now;
            company.UpdatedAt = now;

            return this._context.Company.Add(company).Entity;
        }

        void ICompanyRepository.Delete(int companyId)
        {
            Company company = this._context.Company.FirstOrDefault(c => c.ID == companyId);
            if (company != null)
            {
                this._context.Remove(company);
            }
        }

        Company ICompanyRepository.Get(int companyId)
        {
            return this._context.Company.FirstOrDefault(c => c.ID == companyId);
        }
        Company ICompanyRepository.GetByName(string companyName)
        {
            string _companyName = companyName.ToLower();

            return this._context.Company.FirstOrDefault(c => c.Name == _companyName);
        }

        void ICompanyRepository.Update(Company company)
        {
            Company _company = this._context.Company.FirstOrDefault(c => c.ID == company.ID);

            if (_company == null)
            {
                return;
            }

            _company.Name = company.Name != null ? company.Name : _company.Name;
            _company.Description = company.Description != null ? company.Description : _company.Description;
            _company.UpdatedAt = DateTime.Now;

            this._context.Company.Update(_company);
        }

        int ICompanyRepository.Save()
        {
            return this._context.SaveChanges();
        }
    }
}
