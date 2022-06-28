using close_and_cheap.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace close_and_cheap.JWT
{
    public interface IJwtManager
    {
        public JwtResultDTO GenerateToken(Claim[] claims);
    }
}
