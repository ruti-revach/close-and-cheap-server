
using close_and_cheap.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace close_and_cheap.Services.Interfaces
{
    public interface IClaimService
    {
        public Task<ClaimData> GetClaim(int id);
        public Task<ICollection<ClaimData>> GetClaims();
        public Task<bool> PersistClaimsForUser(User persistUser);
    }
}
