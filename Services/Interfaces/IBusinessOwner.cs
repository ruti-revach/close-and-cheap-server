using close_and_cheap.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace close_and_cheap.Services.Interfaces
{
    public interface IBusinessOwner
    {
        public Task<ResponseDTO> Add(BusinessOwnerDTO businessOwner);

        public Task<List<BusinessOwnerDTO>> GetAll();

        public Task<BusinessOwnerDTO> GetBusinessOwner(int id);

        public Task<ResponseDTO> Update(int id, BusinessOwnerDTO businessOwner);

        public Task<ResponseDTO> DeleteBusinessOwner(int id);
    }
}
