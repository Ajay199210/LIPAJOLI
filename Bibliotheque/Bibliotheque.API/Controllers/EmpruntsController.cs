using Bibliotheque.ApplicationCore.Entites;
using Bibliotheque.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Bibliotheque.ApplicationCore.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bibliotheque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpruntsController : ControllerBase
    {
        private readonly IEmpruntsService _empruntsService;

        public EmpruntsController(IEmpruntsService empruntsService)
        {
            _empruntsService = empruntsService;
        }

        /// <summary>
        /// Retourne la liste des emprunts
        /// </summary>
        /// <remarks>Chaque emprunt est retourné avec ses propriétés de navigation (usager et livre)</remarks>
        /// <response code="200">La liste des emprunts est retournée avec succès</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// 
        // GET: api/<EmpruntsController>
        [HttpGet]
        public async Task<IEnumerable<Emprunt>> Get()
        {
            return await _empruntsService.ObtenirTout();
        }

        /// <summary>
        /// Retourne un emprunt spécifique à partir de son id
        /// </summary>
        /// <remarks>
        ///     Cette méthode pourra être utile pour chercher un emprunt spécifique afin de vérifier 
        ///     si ce dernier existe ou non.
        /// </remarks>
        /// <param name="id">id de l'emprunt à retourner</param>
        /// <response code="200">emprunt trouvé et retourné</response>
        /// <response code="404">emprunt introuvable pour l'id spécifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// 
        // GET api/<EmpruntsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Emprunt emprunt = await _empruntsService.ObtenirSelonId(id);

            if (emprunt == null)
            {
                return NotFound("L'emprunt n'existe pas pour l'identifiant saisi.");
            }
            
            return Ok(emprunt);
        }

        /// <summary>
        /// Ajouter un nouveau emprunt
        /// </summary>
        /// <remarks>
        ///     Les propriétés de navigation ne sont pas affichés lors de l'ajout d'un nouveau emprunt 
        ///     puisqu'on se sert de l'objet RequeteEmprunt dans l'Application Core.
        ///     Si on va laisser ces propriétés, l'application va nous créer un nouveau usager et
        ///     un nouveau livre avec l'id correspondante de chacune de ces objets.
        ///     On ne veut pas modifier la BDD dans ce cas...
        ///     L'annotation 'JsonIgnore' a été utilisé pour les entités livre et usager pour résoudre ce problème.
        /// </remarks>
        /// <param name="requeteEmprunt">L'objet emprunt qui va être créé</param>
        /// <response code="201">L'ajout de l'emprunt est effectué avec succès</response>
        /// <response code="400">L'ajout de l'emprunt est non valide</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// 
        // POST api/<EmpruntsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RequeteEmprunt requeteEmprunt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _empruntsService.Ajouter(requeteEmprunt);
                    return CreatedAtAction("Post", requeteEmprunt);
                }
            }

            catch(BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Retourner un emprunt
        /// </summary>
        /// <remarks>
        ///     La modification dans le contexte de l'application sert à retourner un livre,
        ///     c'est à dire rendre (retourner) un emprunt pour qu'on puisse ajouter un date de retour
        ///     pour ce dernier afin d'enregistrer l'emprunt dans la liste d'historique des emprunts.
        ///     L'exécution de l'opération PUT va retourner l'emprunt à moins qu'il a une date de retour.
        ///     L'affichage de l'emprunt (propriétés de l'emprunt) est omis puisque la date de retour va
        ///     être affectée automatiqument lors de la requête PUT.
        /// </remarks>
        /// <param name="id">id de l'emprunt en cours de modification</param>
        /// <response code="204">Mise à jour effectuée avec succès (emprunt est retourné)</response>
        /// <response code="400">Emprunt est déjà retourné</response>
        /// <response code="404">Emprunt non trouvé</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        ///
        // PUT api/<EmpruntsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id)
        {
            Emprunt empruntAModifie = await _empruntsService.ObtenirSelonId(id);
            
            if (empruntAModifie == null)
            {
                return NotFound("L'emprunt n'existe pas pour l'indentifiant saisi.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _empruntsService.Modifier(empruntAModifie);
                    return NoContent(); // HTTP 204 : Mise à jour effectué avec succès (emprunt retourné)
                }
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Supprimer un emprunt
        /// </summary>
        /// <remarks>
        ///     Taper l'id de l'emprunt à supprimer...
        ///     GET /api/Emprunts/{id} peut nous servir de vérifier les informations 
        ///     d'un emprunt avant sa suppression.
        ///     On ne peut pas supprimer un emprunt déjà rendu, mais on laisse l'utilisateur
        ///     de supprimer un emprunt en cours dans le cas où il a mal choisi l'id (nom) ou/et 
        ///     l'id (titre) du livre.
        /// </remarks>
        /// <param name="id">L'id de l'emprunt qui va être supprimé</param>
        /// <response code="204">Suppression de l'emprunt effectué avec succès</response>
        /// <response code="400">L'emprunt ne peut être supprimé (emprunt existe dans l'historique des emprunts)</response>
        /// <response code="404">L'emprunt est introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        ///
        // DELETE api/<EmpruntsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Emprunt emprunt = await _empruntsService.ObtenirSelonId(id);

            if (emprunt == null)
            {
                return NotFound("Emprunt non trouvé. Veuillez svp réessayer avec un nouveau id.");
            }

            try
            {
                await _empruntsService.Supprimer(emprunt);
                return NoContent();
            }
            catch(BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
