using Bibliotheque.ApplicationCore.Entites;
using Bibliotheque.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bibliotheque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivresController : ControllerBase
    {
        private readonly ILivresService _livresService;

        public LivresController(ILivresService livresService)
        {
            _livresService = livresService;
        }

        /// <summary>Retourne la liste des livres</summary>
        /// <response code="200">La liste des emprunts est retournée avec succès</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// 
        // GET: api/<LivresController>
        [HttpGet]
        public async Task<IEnumerable<Livre>> Get()
        {
            return await _livresService.ObtenirTout();
        }

        /// <summary>Retourne un livre spécifique à partir de son id</summary>
        /// <param name="id">id de l'livre à retourner</param>
        /// <response code="200">livre trouvé et retourné</response>
        /// <response code="404">livre introuvable pour l'id spécifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// 
        // GET api/<LivresController>/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Livre livre = await _livresService.ObtenirSelonId(id);
            if ( livre == null)
            {
                return NotFound("Livre non trouvé. Vérifiez svp l'id saisi et essayer de nouveau.");
            }

            return Ok(livre);
        }
    }
}
