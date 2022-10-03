using AngularUrlShortener.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AngularUrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly ILogger<AccountController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ISettingsRepository _settingsRepo;

        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ISettingsRepository settingsRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _settingsRepo = settingsRepository;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                // By Email.
                IdentityUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        return Ok(new { Username = user.UserName });
                    }
                }

                // By Username.
                user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        return Ok(new { Username = user.UserName });
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid credentials!");
            return Unauthorized();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(Register model)
        {
            if (model.Password != model.Password2)
            {
                ModelState.AddModelError(string.Empty, "Password does not match!");
                return BadRequest(ModelState);
            }

            if (!_settingsRepo.Get().AllowSignup.Value)
            {
                ModelState.AddModelError(string.Empty, "Signup's have been closed.");
                return BadRequest(ModelState);
            }

            IdentityUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "A user with that email already exists!");
                return BadRequest(ModelState);
            }

            user = new IdentityUser(model.Username);
            user.Email = model.Email;
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return await Login(
                    new Models.Login()
                    {
                        Email = user.Email,
                        Password = model.Password
                    });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Get()
        {
            List<IdentityUser> users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("{userId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Disable([FromRoute] string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user != null && !await _userManager.IsInRoleAsync(user, "Administrator"))
            {
                await _userManager.SetLockoutEnabledAsync(user, false);
                await _userManager.SetLockoutEndDateAsync(user, new System.DateTimeOffset(new DateTime(2100, 0, 0)));
                return StatusCode(204);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Me()
        {
            if (User.Identity != null && User.Identity.Name != null)
            {
                IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    return Ok(new { Username = user.UserName });
                }
            }

            return Unauthorized();
        }
    }
}
