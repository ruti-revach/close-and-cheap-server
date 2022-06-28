﻿
using close_and_cheap.Data;
using close_and_cheap.Data.DTO;
using close_and_cheap.Data.Entities;
using close_and_cheap.Entities;
using close_and_cheap.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static close_and_cheap.Extensions.ConstantsRoles;

namespace close_and_cheap.Services
{
    public class AdminService : IAdminService
    {
        private readonly DBContext m_db;
        private readonly IClaimService claim;
        public AdminService(DBContext db, IClaimService claim)
        {
            m_db = db;
            this.claim = claim;
        }

        public async Task<ResponseDTO> Add(AdminDTO admin)
        {
            Role role = await m_db.Role.Where(x => x.Id == RolesId.Admin).FirstOrDefaultAsync();

            Admin AdminFromDB = new Admin(0, admin.Name, admin.Email
            , admin.Password, RolesId.Admin);

            await m_db.Admins.AddAsync(AdminFromDB);
            
            int c = await m_db.SaveChangesAsync();
            
            ResponseDTO response = new ResponseDTO();

            if (c > 0)
            {
                bool Affected = await claim.PersistClaimsForUser(AdminFromDB);
                if (Affected)
                {
                    response.Status = StatusCode.Success;
                    return response;
                }
                response.Status = StatusCode.Warning;
                response.StatusText = "admin adedd BUT presist Not Apply";
                return response;
            }
            response.Status = StatusCode.Faild;
            return response;
        }
        public async Task<ResponseDTO> DeleteAdmin(int id)
        {
            AdminDTO admin = await GetAdmin(id);

            if (admin == null)
            {
                return new ResponseDTO() { StatusText = "this object not exists", Status = StatusCode.Faild };
            }

            m_db.Admins.Remove(new Admin { Id = admin.Id });
            int c = await m_db.SaveChangesAsync();
            ResponseDTO response = new ResponseDTO();
            if (c > 0)
            {
                response.StatusText = "Successfully object deleted";
                response.Status = StatusCode.Success;
            }
            else
            {
                response.Status = StatusCode.Error;
            }
            return response;
        }

        public async Task<AdminDTO> GetAdmin(int id)
        {
            var admin = await m_db.Admins.Select(s => new AdminDTO()
            {

                Name = s.Name,
                Id = s.Id,
                Email = s.Email,
                Password = s.Password,
            }).FirstOrDefaultAsync(a=> a.Id == id);
            return admin;
        }

        public async Task<List<AdminDTO>> GetAll()
        {
            var admins = await m_db.Admins.Select(s => new AdminDTO()
            {
                Name = s.Name,
                Id = s.Id,
                Email = s.Email,
                Password = s.Password,
            }).ToListAsync();
            return admins;
        }

        public async Task<ResponseDTO> Update(int id, AdminDTO admin)
        {
            Admin AdminFromDB = new Admin();
            AdminDTO OriginalAdmin = await GetAdmin(id);

            if (AdminFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatusCode.Error,
                    StatusText = $"Item with id {id} not found in DB"
                };
            }

            AdminFromDB.Email = OriginalAdmin.Email;

            AdminFromDB.Name = admin.Name ?? OriginalAdmin.Name;
            AdminFromDB.Password = admin.Password ?? OriginalAdmin.Password;
            AdminFromDB.Id = Convert.ToInt32(OriginalAdmin.Id.ToString());
            AdminFromDB.RoleId = RolesId.Admin;

            m_db.Entry(AdminFromDB).State = EntityState.Modified;

            int c = await m_db.SaveChangesAsync();
            ResponseDTO response = new ResponseDTO();
            if (c > 0)
            {
                response.StatusText = c + " Admin affected";
                response.Status = StatusCode.Success;
            }
            else
            {
                response.Status = StatusCode.Faild;
                response.StatusText = "faild no Admin affacted";
            }
            return response;
        }
        public async Task<bool> Validation(string emai)
        {
            if (await m_db.Admins.Where(i => i.Email == emai).FirstOrDefaultAsync() != null)
            {
                return true;
            }
            return false;
        }
    }
}
