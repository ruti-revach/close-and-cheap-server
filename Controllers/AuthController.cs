using System;
using System.Threading.Tasks;
using close_and_cheap.Data.DTO;
using close_and_cheap.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace close_and_cheap.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost, Route("login")]
        public async Task<ActionResult<JwtResultDTO>> Login(AuthUserDTO authUserDTO)
        {
            try
            {

                if (authUserDTO.Email != null && authUserDTO.Password != null)
                {
                    JwtResultDTO jwtResultDto = await _authService.Authentication(authUserDTO);
                    if (jwtResultDto != null)
                    {
                        return Ok(jwtResultDto);
                    }
                    else return BadRequest("email or password is not correct");

                }

                return BadRequest("one or more fields is empty");

            }
            catch (Exception)
            {
                return BadRequest("Erorr server");
            }

        }
    }
}
