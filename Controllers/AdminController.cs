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
    [Authorize(Roles = RolesName.Admin)]// Only admin has access, but not exactly... see below
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService service;
        public AdminController(IAdminService service)
        {
            this.service = service;
        }
        [HttpGet]
        [Route("{id?}")]
        public async Task<ActionResult> Get(int id = 0)//קבלה
        {
            try
            {
                if (id < 1)
                {
                    List<AdminDTO> result = await service.GetAll();

                    return Ok(result);
                }
                AdminDTO resultAdmin = await service.GetAdmin(id);
                return Ok(resultAdmin);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<ActionResult> PostAdmin(AdminDTO admin)
        {
            try
            {
                if (admin != null || admin.Password != null || admin.Email != null)
                {
                    if (!await service.Validation(admin.Email))
                    {
                        ResponseDTO respone = await service.Add(admin);
                        if (respone.Status == Data.DTO.StatusCode.Success)
                        {
                            return Created("", null);
                        }
                        return BadRequest(respone);
                    }
                    return BadRequest(new ResponseDTO { StatusText = "mail already exist" });
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return BadRequest("Server error");
            }

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutAdmin(int id, AdminDTO admin)
        {
            ResponseDTO response = new ResponseDTO();
            if (id != admin.Id)
            {
                response.StatusText = "id does not match";
                return BadRequest(response);
            }

            try
            {
                response = await service.Update(id, admin);
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
        public async Task<ActionResult> DeleteAdmin(int id)
        {
            try
            {
                ResponseDTO response = await service.DeleteAdmin(id);
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
