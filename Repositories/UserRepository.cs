using System;
using System.Linq;
using DatacircleAPI.Database;
using DatacircleAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DatacircleAPI.Repositories
{
    public class UserRepository : IUserRepository
    {        
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        User IUserRepository.FindByVerificationToken(string verificationToken)
        {
            return this._context.Users.First(user => user.VerificationKey == verificationToken);
        }

        int IUserRepository.Save()
        {
            return this._context.SaveChanges();
        }
    }
}
