
using close_and_cheap.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace close_and_cheap.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ResponseDTO> Add(UserDTO user);

        public Task<List<UserDTO>> GetAll();

        public Task<UserDTO> GetUser(int id);

        public Task<ResponseDTO> Update(int id, UserDTO user);

        public Task<ResponseDTO> DeleteUser(int id);
        public Task<bool> Validation(string emai);
    }
}
