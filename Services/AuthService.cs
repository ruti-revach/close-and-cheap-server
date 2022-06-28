using close_and_cheap.Data;
using close_and_cheap.Data.DTO;
using close_and_cheap.Data.Entities;
using close_and_cheap.JWT;
using close_and_cheap.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace close_and_cheap.Services
{
    public class AuthService : IAuthService
    {
        private readonly DBContext m_db;
        private readonly IJwtManager _jwtManager;

        public AuthService(DBContext db, IJwtManager _jwtManager)
        {
            m_db = db;
            this._jwtManager = _jwtManager;
        }

        public async Task<JwtResultDTO> Authentication(AuthUserDTO authUserDTO)
        {
            try
            {
                var user = await m_db.Users.Include(u => u.Claims)
                    .Include(u => u.Role).Where(u => u.Email.ToLower() == authUserDTO.Email.ToLower()
                    && u.Password == authUserDTO.Password).FirstOrDefaultAsync();

                if (user != null)
                {
                    List<Claim> claims = new List<Claim>();

                    foreach (ClaimData item in user.Claims)
                    {
                        claims.Add(new Claim(item.Type, item.Value));
                    }
                    JwtResultDTO jwtResult = _jwtManager.GenerateToken(claims.ToArray());
                    return jwtResult;
                }

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }


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
