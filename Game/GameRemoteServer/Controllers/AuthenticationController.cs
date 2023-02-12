using GameRemoteServer.Models;
using GameRemoteServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace GameRemoteServer.Controllers
{
    [Route("api/auth")]
    [Authorize]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private const string RTOKEN_COOKIE = "RefreshToken";

        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public AuthenticationController(IUserService userService,
            ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> SignUp([FromBody] ReqSignUpDTO request)
        {
            await userService.CreateUser(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] ReqAuthenticateDTO request)
        {
            var response = tokenService.Authenticate(request);
            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromQuery] string? tkn)
        {
            var token = Request.Cookies[RTOKEN_COOKIE] ?? tkn;
            if(string.IsNullOrEmpty(token)) return Unauthorized();

            var response = tokenService.RefreshToken(token);
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken()
        {
            //Accept refresh token in cookie
            var token = Request.Cookies[RTOKEN_COOKIE];

            if(string.IsNullOrEmpty(token)) return BadRequest();

            tokenService.RevokeToken(token);
            return Ok();
        }

        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            IEnumerable<UserDTO> response = userService.GetAllUsers();
            return Ok(response);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOoptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append(RTOKEN_COOKIE, token, cookieOoptions);
        }
    }
}