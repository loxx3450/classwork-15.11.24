using AutoMapper;
using classwork.Core.Interfaces;
using classwork_15._11._24.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace classwork_15._11._24.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserDTO user)
        {
            try
            {
                await _authService.Register(user.Email, user.Password);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDTO>> Login([FromBody] UserDTO user)
        {
            var (loggedUser, token) = await _authService.Login(user.Email, user.Password);

            if (loggedUser is null)
                return NotFound();

            var response = new ResponseDTO()
            {
                User = loggedUser,
                Token = token
            };

            return Ok(response);
        }
    }
}
