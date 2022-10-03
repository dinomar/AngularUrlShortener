using AngularUrlShortener.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AngularUrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> logger;
        private readonly ISettingsRepository _settingsRepo;

        public SettingsController(ILogger<SettingsController> logger, ISettingsRepository settingsRepository)
        {
            this.logger = logger;
            _settingsRepo = settingsRepository;
        }

        [HttpGet("[action]")]
        public IActionResult Hostname()
        {
            return Ok(_settingsRepo.Get().Hostname);
        }

        [HttpPut("[action]")]
        public IActionResult Hostname(Settings model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Hostname))
                {
                    _settingsRepo.Update(model);
                    return Ok(_settingsRepo.Get().Hostname);
                }
            }

            return BadRequest();
        }

        // TODO: Get settings
        // TODO: Update settings
    }
}
