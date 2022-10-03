using AngularUrlShortener.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularUrlShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinksController : ControllerBase
    {
        private readonly ILogger<LinksController> _logger;
        private readonly ILinksRepository _repo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISettingsRepository _settingsRepo;

        public LinksController(
            ILogger<LinksController> logger,
            ILinksRepository repository,
            UserManager<IdentityUser> userManager,
            ISettingsRepository settingsRepository)
        {
            _logger = logger;
            _repo = repository;
            _userManager = userManager;
            _settingsRepo = settingsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Return all links belonging to user.
            if (User != null)
            {
                IdentityUser user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var links = _repo.GetAll(user.Id);
                    foreach (Link link in links)
                    {
                        link.ShortUrl = new Uri(new Uri(_settingsRepo.Get().Hostname), link.ShortUrl).ToString();
                    }

                    return Ok(links);
                }
            }

            return Unauthorized();
        }

        [HttpGet("{url}")]
        public IActionResult Get([FromRoute] string url)
        {
            Link link = _repo.Get(url);

            if (link != null)
            {
                link.UserId = string.Empty;
                link.ShortUrl = new Uri(new Uri(_settingsRepo.Get().Hostname), link.ShortUrl).ToString();
                return Ok(link);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Link model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_settingsRepo.Get().AllowAnonymousLinkCreation.Value && !User.Identity.IsAuthenticated)
            {
                Link link = _repo.Update(model);
                link.ShortUrl = new Uri(new Uri(_settingsRepo.Get().Hostname), link.ShortUrl).ToString();
                return Created($"api/links/{link.Id}", link);
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    IdentityUser user = await _userManager.GetUserAsync(User);
                    model.UserId = user.Id;

                    Link link = _repo.Update(model);
                    link.ShortUrl = new Uri(new Uri(_settingsRepo.Get().Hostname), link.ShortUrl).ToString();
                    return Created($"api/links/{link.Id}", link);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, Link model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (User != null)
            {
                IdentityUser user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    Link link = _repo.Get(id);
                    if (link != null && link.UserId == user.Id)
                    {
                        if (link.UserId == user.Id)
                        {
                            link = _repo.Update(link);
                            link.ShortUrl = new Uri(new Uri(_settingsRepo.Get().Hostname), link.ShortUrl).ToString();
                            return StatusCode(204, link);
                        }
                        else
                        {
                            return Unauthorized();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }

            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (User != null)
            {
                IdentityUser user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    Link link = _repo.Get(id);
                    if (link != null)
                    {
                        if (link.UserId == user.Id)
                        {
                            link = _repo.Delete(id);
                            return StatusCode(204);
                        }
                        else
                        {
                            return Unauthorized();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }

            return Unauthorized();
        }
    }
}
