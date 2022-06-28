
using close_and_cheap.Data;
using close_and_cheap.Data.DTO;
using close_and_cheap.Data.Entities;
using close_and_cheap.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static close_and_cheap.Extensions.ConstantsRoles;

namespace close_and_cheap.Services
{
    public class UserService : IUserService
    {

        private readonly DBContext m_db;
        private readonly IClaimService claim;
        public UserService(DBContext db, IClaimService claim)
        {
            m_db = db;
            this.claim = claim;
        }

        public async Task<ResponseDTO> Add(UserDTO user)
        {
            Role role = await m_db.Role.Where(x => x.Id == RolesId.User).FirstOrDefaultAsync();

            User UserFromDB = new User(0, user.Name, user.Email, user.Password
            , user.Address, RolesId.User);

            await m_db.Users.AddAsync(UserFromDB);
            
            int c = await m_db.SaveChangesAsync();
            
            ResponseDTO response = new ResponseDTO();

            if (c > 0)
            {
                bool Affected = await claim.PersistClaimsForUser(UserFromDB);
                if (Affected)
                {
                    response.Status = StatusCode.Success;
                    return response;
                }
                response.Status = StatusCode.Warning;
                response.StatusText = "users adedd BUT presist Not Apply";
                return response;
            }
            response.Status = StatusCode.Faild;
            return response;
        }
        public async Task<ResponseDTO> DeleteUser(int id)
        {
            UserDTO user = await GetUser(id);

            if (user == null)
            {
                return new ResponseDTO() { StatusText = "this object not exists", Status = StatusCode.Faild };
            }

            m_db.Users.Remove(new User { Id = user.Id });
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

        public async Task<UserDTO> GetUser(int id)
        {
            var user = await m_db.Users.Select(s => new UserDTO()
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Password = s.Password,
                Address = s.Address,
            }).FirstOrDefaultAsync(a=> a.Id == id);
            return user;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var users = await m_db.Users.Select(s => new UserDTO()
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Password = s.Password,
                Address = s.Address,
            }).ToListAsync();
            return users;
        }

        public async Task<ResponseDTO> Update(int id, UserDTO user)
        {
            User UserFromDB = new User();
            UserDTO OriginalUser = await GetUser(id);

            if (UserFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatusCode.Error,
                    StatusText = $"Item with id {id} not found in DB"
                };
            }

            UserFromDB.Name = OriginalUser.Name;
            UserFromDB.Email = OriginalUser.Email;
            UserFromDB.Address = user.Address ?? OriginalUser.Address;
            UserFromDB.Password = user.Password ?? OriginalUser.Password;
            UserFromDB.Id = Convert.ToInt32(OriginalUser.Id.ToString());
            UserFromDB.RoleId = RolesId.User;

            m_db.Entry(UserFromDB).State = EntityState.Modified;

            int c = await m_db.SaveChangesAsync();
            ResponseDTO response = new ResponseDTO();
            if (c > 0)
            {
                response.StatusText = c + " User affected";
                response.Status = StatusCode.Success;
            }
            else
            {
                response.Status = StatusCode.Faild;
                response.StatusText = "faild no User affacted";
            }
            return response;
        }
        public async Task<bool> Validation(string emai)
        {
            if (await m_db.Users.Where(i => i.Email == emai).FirstOrDefaultAsync() != null)
            {
                return true;
            }
            return false;
        }
    }
}
