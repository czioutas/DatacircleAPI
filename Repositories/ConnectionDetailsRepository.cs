using System;
using System.Linq;
using DatacircleAPI.Database;
using DatacircleAPI.Models;

namespace DatacircleAPI.Repositories
{
    public class ConnectionDetailsRepository : IConnectionDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public ConnectionDetailsRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        ConnectionDetails IConnectionDetailsRepository.Create(ConnectionDetails connectionDetails)
        {
            DateTime now = DateTime.Now;
            
            connectionDetails.CreatedAt = now;
            connectionDetails.UpdatedAt = now;
            
            return this._context.ConnectionDetails.Add(connectionDetails).Entity;
        }

        void IConnectionDetailsRepository.Delete(int connectionDetailsId)
        {
            ConnectionDetails connectionDetails = this._context.ConnectionDetails
                .FirstOrDefault(cd => cd.ID == connectionDetailsId);
                
            if (connectionDetails != null) {
                this._context.ConnectionDetails.Remove(connectionDetails);
            }        
        }

        ConnectionDetails IConnectionDetailsRepository.Get(int connectionDetailsId)
        {
            return this._context.ConnectionDetails.FirstOrDefault(ds => ds.ID == connectionDetailsId);
        }

        int IConnectionDetailsRepository.Save()
        {
           return this._context.SaveChanges();
        }

        void IConnectionDetailsRepository.Update(ConnectionDetails connectionDetails)
        {
            connectionDetails.UpdatedAt = DateTime.Now;
            this._context.ConnectionDetails.Update(connectionDetails);
        }
    }
}
