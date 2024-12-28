using AppInvWebSharedLibrary.Contracts;
using AppInvWebSharedLibrary.DTOs;
using AppInvWebSharedLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppInvWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount accountrepo;

        public AccountController(IAccount accountrepo)
        {
            this.accountrepo = accountrepo;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegisterDTO model)
        {
            var result = await accountrepo.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginDTO model)
        {
            var result = await accountrepo.LoginAsync(model);
            return Ok(result);
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> LogoutAsync()
        {
            var result = await accountrepo.LogoutAsync();
            return Ok(result);
        }
    }
}
