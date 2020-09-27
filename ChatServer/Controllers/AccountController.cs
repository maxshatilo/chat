namespace ChatServer.Controllers
{
    using System.Threading.Tasks;
    using AppServices.UserService.Interfaces;
    using Domain.ViewModels.AccountViewModels;
    using Infrastructure.Helpers.Identity.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTTokenProvider _jwtTokenProvider;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, IJWTTokenProvider jwtTokenProvider,
            ILogger<AccountController> logger, IConfiguration configuration)
        {
            _userService = userService;
            _jwtTokenProvider = jwtTokenProvider;
            _logger = logger;
            _configuration = configuration;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _userService.PasswordSignInAsync(model.UserName, model.Password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = await _jwtTokenProvider.GetJWTToken(model.UserName);

                if (!string.IsNullOrEmpty(token))
                {
                    _logger.LogInformation($"User {model.UserName} logged in.");

                    return Ok(new {token});
                }

                return Unauthorized();
            }

            return Unauthorized();
        }

        [Route("user/{userName}")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await _userService.GetByUserName(userName);
            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest();
        }

        [Route("register")]
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateAsync(model.UserName, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Created a new user account {model.UserName}");

                    var loginResult = await _userService.PasswordSignInAsync(model.UserName, model.Password, true, lockoutOnFailure: false);

                    if (loginResult.Succeeded)
                    {
                        var token = await _jwtTokenProvider.GetJWTToken(model.UserName);

                        if (!string.IsNullOrEmpty(token))
                        {
                            _logger.LogInformation($"User {model.UserName} logged in.");

                            return Ok(new{token});
                        }

                        return Unauthorized();
                    }

                    return Unauthorized();
                }

                _logger.LogInformation($"There was an error during creating new user account {model.UserName}");

                return BadRequest();
            }

            return BadRequest();
        }
    }
}
