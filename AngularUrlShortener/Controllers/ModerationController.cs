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
    public class ModerationController : ControllerBase
    {
        private readonly ILogger<ModerationController> _logger;
        private readonly ILinksRepository _linksRepo;
        private readonly ISettingsRepository _settingsRepo;

        public ModerationController(ILogger<ModerationController> logger, ILinksRepository linksRepository, ISettingsRepository settingsRepository)
        {
            _logger = logger;
            _linksRepo = linksRepository;
            _settingsRepo = settingsRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            // Return all links.
            var links = _linksRepo.GetAll()
                        .Select(l => l.Url = new Uri(new Uri(_settingsRepo.Get().Hostname), l.ShortUrl).ToString());

            return Ok(_linksRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            Link link = _linksRepo.Get(id);
            link.ShortUrl = new Uri(new Uri(_settingsRepo.Get().Hostname), link.ShortUrl).ToString();

            return Ok(link);
        }

        [HttpPut("{id}")]
        public IActionResult PutAsync([FromRoute] int id, Link model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (User != null)
            {
                Link link = _linksRepo.Get(id);
                if (link != null)
                {
                    link = _linksRepo.Update(link);
                    link.ShortUrl = new Uri(new Uri(_settingsRepo.Get().Hostname), link.ShortUrl).ToString();
                    return StatusCode(204, link);
                }
                else
                {
                    return NotFound();
                }
            }

            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAsync(int id)
        {
            if (User != null)
            {
                Link link = _linksRepo.Get(id);
                if (link != null)
                {
                    link = _linksRepo.Delete(id);
                    return StatusCode(204);
                }
                else
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }
    }
}
