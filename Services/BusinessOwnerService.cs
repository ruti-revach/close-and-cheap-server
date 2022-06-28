using close_and_cheap.Data;
using close_and_cheap.Data.DTO;
using close_and_cheap.Data.Entities;
using close_and_cheap.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace close_and_cheap.Services
{
    public class BusinessOwnerService : IBusinessOwner
    {
        private readonly DBContext m_db;

        public BusinessOwnerService(DBContext db)
        {
            m_db = db;
        }
        public async Task<ResponseDTO> Add(BusinessOwnerDTO businessOwner)
        {
            BusinessOwner BusinessOwnerFromDB = new BusinessOwner(0, businessOwner.Name,businessOwner.Address
                ,businessOwner.Phone,businessOwner.CategoryId);

            await m_db.BusinessOwners.AddAsync(BusinessOwnerFromDB);

            int c = await m_db.SaveChangesAsync();

            ResponseDTO response = new ResponseDTO();

            if (c > 0)
            {

                response.Status = StatusCode.Success;
                return response;

            }
            response.Status = StatusCode.Faild;
            return response;
        }

        public async Task<ResponseDTO> DeleteBusinessOwner(int id)
        {
            BusinessOwnerDTO businessOwner = await GetBusinessOwner(id);

            if (businessOwner == null)
            {
                return new ResponseDTO() { StatusText = "this object not exists", Status = StatusCode.Faild };
            }

            m_db.BusinessOwners.Remove(new BusinessOwner { Id = businessOwner.Id });
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

        public async Task<List<BusinessOwnerDTO>> GetAll()
        {
            var businessOwners = await m_db.BusinessOwners.Select(s => new BusinessOwnerDTO()
            {

                Id = s.Id,
                Address = s.Address,
                CategoryId = s.CategoryId,
                Name = s.Name,
                Phone = s.Phone
            }).ToListAsync();
            return businessOwners;
        }

        public async Task<BusinessOwnerDTO> GetBusinessOwner(int id)
        {
            var businessOwner = await m_db.BusinessOwners.Select(s => new BusinessOwnerDTO()
            {

                Id = s.Id,
                Address = s.Address,
                CategoryId = s.CategoryId,
                Name = s.Name,
                Phone = s.Phone
            }).FirstOrDefaultAsync(a => a.Id == id);
            return businessOwner;
        }

        public async Task<ResponseDTO> Update(int id, BusinessOwnerDTO businessOwner)
        {
            BusinessOwner businessOwnerFromDB = new BusinessOwner();
            BusinessOwnerDTO OriginalBusinessOwner = await GetBusinessOwner(id);

            if (businessOwnerFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatusCode.Error,
                    StatusText = $"Item with id {id} not found in DB"
                };
            }

      
            businessOwnerFromDB.Name = businessOwnerFromDB.Name ?? OriginalBusinessOwner.Name;
            businessOwnerFromDB.Address = businessOwnerFromDB.Address ?? OriginalBusinessOwner.Address;
            businessOwnerFromDB.Phone = businessOwner.Phone?? OriginalBusinessOwner.Phone;
            businessOwnerFromDB.CategoryId = Convert.ToInt32(businessOwner.CategoryId.ToString() ?? OriginalBusinessOwner.CategoryId.ToString());
            businessOwnerFromDB.Id = Convert.ToInt32(OriginalBusinessOwner.Id.ToString());

            m_db.Entry(businessOwnerFromDB).State = EntityState.Modified;

            int c = await m_db.SaveChangesAsync();
            ResponseDTO response = new ResponseDTO();
            if (c > 0)
            {
                response.StatusText = c + " BusinessOwner affected";
                response.Status = StatusCode.Success;
            }
            else
            {
                response.Status = StatusCode.Faild;
                response.StatusText = "faild no BusinessOwner affacted";
            }
            return response;
        }
    }
}
