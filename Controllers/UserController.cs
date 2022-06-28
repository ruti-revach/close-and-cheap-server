using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using close_and_cheap.Data.DTO;
using close_and_cheap.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static close_and_cheap.Extensions.ConstantsRoles;

namespace close_and_cheap.Controllers
{
    [Authorize(Roles = RolesName.Admin + "," + RolesName.User)]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }


        //In case, that request only from admin
        [Authorize(Roles = RolesName.Admin)]
        public async Task<ActionResult> GetAll()
        {
            try
            {

                List<UserDTO> result = await service.GetAll();
                if (result != null) return Ok(result);

                return NotFound("No Results");
            }
            catch
            {
                return BadRequest("Error in server");
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                UserDTO resultUser = await service.GetUser(id);
                if (resultUser != null) return Ok(resultUser);

                return NotFound("No Result");
            }
            catch
            {
                return BadRequest("Error in server");
            }

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> PostUser(UserDTO user)
        {
            try
            {

                if (user != null || user.Password != null || user.Email != null )
                {
                    if (!await service.Validation(user.Email))
                    {
                        ResponseDTO respone = await service.Add(user);
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
        public async Task<ActionResult> PutUser(int id, UserDTO user)
        {
            ResponseDTO response = new ResponseDTO();
            if (id != user.Id)
            {
                response.StatusText = "id does not match";
                return BadRequest(response);
            }

            try
            {
                response = await service.Update(id, user);
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
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                ResponseDTO response = await service.DeleteUser(id);
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
