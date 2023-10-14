using Bibliotheque.ApplicationCore.Entites;
using Bibliotheque.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bibliotheque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsagersController : ControllerBase
    {
        private readonly IUsagersService _usagersService;

        public UsagersController(IUsagersService usagersService)
        {
            _usagersService = usagersService;
        }

        // GET: api/<UsagersController>
        [HttpGet]
        public async Task<IEnumerable<Usager>> Get()
        {
            return await _usagersService.ObtenirTout();
        }

        /// <summary>
        /// Retourne un usager spécifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id de l'usager à retourner</param>
        /// <response code="200">usager trouvé et retourné</response>
        /// <response code="404">usager introuvable pour l'id spécifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// 
        // GET api/<UsagersController>/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Usager usager= await _usagersService.ObtenirSelonId(id);
            if (usager == null)
            {
                return NotFound("Usager non trouvé. Vérifier svp l'id saisi et essayer de nouveau.");
            }

            return Ok(usager);
        }
    }
}
