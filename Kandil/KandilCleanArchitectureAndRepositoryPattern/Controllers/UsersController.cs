
using Kandil.Application.DTO;
using Kandil.Application.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace kandil.Web.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }





        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var tokenDto = await _userRepo.Login(model);
            if (tokenDto == null || string.IsNullOrEmpty(tokenDto.AccessToken))
            {

                return BadRequest("اسم المستخدم أو كلمة المرور غير صالحة.");
            }
            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> GetNewTokenFromRefreshToken([FromBody] TokenDTO tokenDTO)
        {
            if (ModelState.IsValid)
            {
                var tokenDTOResponse = await _userRepo.RefreshAccessToken(tokenDTO);
                if (tokenDTOResponse==null || string.IsNullOrEmpty(tokenDTOResponse.AccessToken))
                {
                    return BadRequest("Token Invalid");
                }
                return Ok(tokenDTOResponse);
            }
            else
            {
                return BadRequest("Invalid Input");
            }
            
        }


        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] TokenDTO tokenDTO)
        {
            
            if (ModelState.IsValid)
            {
                await _userRepo.RevokeRefreshToken(tokenDTO);

                return Ok();
                
            }

            return BadRequest("Invalid Input");
        }

    }
}
