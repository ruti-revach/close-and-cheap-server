using close_and_cheap.Data.DTO;
using System.Threading.Tasks;

namespace close_and_cheap.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<JwtResultDTO> Authentication(AuthUserDTO authUserDTO);

        public Task<bool> Validation(string emai);
    }
}
