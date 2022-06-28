using close_and_cheap.Data.DTO;
using close_and_cheap.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static close_and_cheap.Extensions.ConstantsRoles;

namespace close_and_cheap.Controllers
{
    [Authorize(Roles = RolesName.Admin)]
    public class BusinessOwnerController : ControllerBase
    {
        private readonly IBusinessOwner service;
        public BusinessOwnerController(IBusinessOwner service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("{id?}")]
        public async Task<ActionResult> Get(int id = 0)
        {
            try
            {
                if (id < 1)
                {
                    List<BusinessOwnerDTO> result = await service.GetAll();
                    return Ok(result);
                }
                BusinessOwnerDTO resultBusinessOwner = await service.GetBusinessOwner(id);
                return Ok(resultBusinessOwner);
            }
            catch
            {
                return BadRequest("Error in server");
            }

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> PostBusinessOwner(BusinessOwnerDTO businessOwner)
        {
            try
            {

                ResponseDTO respone = await service.Add(businessOwner);
                if (respone.Status == Data.DTO.StatusCode.Success)
                {
                    return Created("", null);
                }
                return BadRequest(respone);

            }
            catch (Exception)
            {

                return BadRequest("Server error");
            }

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutBusinessOwner(int id, BusinessOwnerDTO businessOwner)
        {
            ResponseDTO response = new ResponseDTO();
            if (id != businessOwner.Id)
            {
                response.StatusText = "id does not match";
                return BadRequest(response);
            }

            try
            {
                response = await service.Update(id, businessOwner);
                if (response.Status == Data.DTO.StatusCode.Success)
                {
                    return Ok(response);
                }
            }
            catch
            {
                response.Status = Data.DTO.StatusCode.Error;
                response.StatusText = "ERROR";
                return BadRequest(response);
            }
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteBusinessOwner(int id)
        {
            try
            {
                ResponseDTO response = await service.DeleteBusinessOwner(id);
                if (response.Status == Data.DTO.StatusCode.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception)
            {

                return BadRequest("Server error");
            }

        }
    }
}
