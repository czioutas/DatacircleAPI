using System;
using System.Linq;
using DatacircleAPI.Database;
using DatacircleAPI.Models;
using DatacircleAPI.Repositories;
using DatacircleAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DatacircleAPI.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        Role IRoleRepository.Create(Role role)
        {
            DateTime now = DateTime.Now;
            
            role.CreatedAt = now;
            role.UpdatedAt = now;
            
            return this._context.Roles.Add(role).Entity;            
        }

        void IRoleRepository.Delete(int roleId)
        {
            var role = this._context.Roles.FirstOrDefault(r => r.Id == roleId);
            if (role != null) {
                this._context.Remove(role);
            }
        }

        Role IRoleRepository.Get(int roleId)
        {
            return this._context.Roles.FirstOrDefault(r => r.Id == roleId);
        }

        void IRoleRepository.Update(Role role)
        {            
            var _role = this._context.Roles
            .Where(c => c.Id == role.Id).FirstOrDefault<Role>();

            _role.Admin =  role.Admin;
            _role.DatasourceRead =  role.DatasourceRead;
            _role.DatasourceWrite =  role.DatasourceWrite;
            _role.MetricRead =  role.MetricRead;
            _role.MetricWrite =  role.MetricWrite;
            _role.WidgetRead =  role.WidgetRead;
            _role.WidgetWrite =  role.WidgetWrite;
            _role.DashboardRead = role.DashboardRead;
            _role.DashboardWrite = role.DashboardWrite;       
                        
            _role.UpdatedAt = DateTime.Now;
             
            this._context.Roles.Update(_role);
        }

        int IRoleRepository.Save()
        {
           return this._context.SaveChanges();
        }

        public Role getDefaultNewRole()
        {
            return new Role {
                Admin = true,
                WidgetRead = true,
                WidgetWrite = true,
                MetricRead = true,
                MetricWrite = true,
                DatasourceWrite = true,
                DatasourceRead = true,
                DashboardRead = true,
                DashboardWrite = true,
                Name = "Default Admin Role"
            };
        }
    }
}
