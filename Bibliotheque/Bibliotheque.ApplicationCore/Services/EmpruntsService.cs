using Bibliotheque.ApplicationCore.DTO;
using Bibliotheque.ApplicationCore.Entites;
using Bibliotheque.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Bibliotheque.ApplicationCore.Services
{
    public class EmpruntsService : IEmpruntsService
    {
        private IAsyncRepository<Emprunt> _empruntsRepository;
        private ILivresService _livresService;
        private IUsagersService _usagersService;
        private IConfiguration _config;

        public EmpruntsService(IAsyncRepository<Emprunt> empruntsRepository, ILivresService livresService, 
                IUsagersService usagersSerivce,
                IConfiguration config)
        {
            _empruntsRepository = empruntsRepository;
            _livresService = livresService;
            _usagersService = usagersSerivce;
            _config = config;
        }

        public async Task Ajouter(RequeteEmprunt requeteEmprunt)
        {
            Emprunt emprunt = new Emprunt
            {
                LivreID = requeteEmprunt.LivreID,
                UsagerID = requeteEmprunt.UsagerID,
                DateEmprunt = DateTime.Now,
            };

            Livre livre = await _livresService.ObtenirSelonId(emprunt.LivreID);
            Usager usager = await _usagersService.ObtenirSelonId(emprunt.UsagerID);

            // Vérifions d'abord si l'usager et le livre existe avant l'opération d'ajout
            if(livre == null || usager == null)
            {
                throw new BadHttpRequestException("Veuillez svp vérifier l'id de l'usager et du livre et réssayer de nouveau.");
            }

            // A trois défaillances, l’usager ne doit plus être capable d’emprunter
            if(usager.Defaillance >= 3)
            {
                // HTTP 400 (côté client)
                throw new BadHttpRequestException("L'usager ne peut pas emprunter un livre puisqu'il a 3 défaillance.");
            }

            // S'il ne reste plus de livres, l'usager ne peut pas emprunter
            if(livre.Quantite == 0)
            {
                throw new BadHttpRequestException($"Il n' y a plus d'exemplaires pour le livre '{livre.Titre}' pour le moment. " +
                    $"Veuillez svp essayer plus tard.");
            }

            // Si l'usager a au moins un emprunt dans l'historique des emprunts
            if(usager.Emprunts.Any())
            {
                // Un usager peut emprunter trois livres (3 emprunts en cours) au maximum
                if (usager.Emprunts.Count(e => e.DateRetour == null) >= 3)
                {
                    throw new BadHttpRequestException("Un usager peut avoir jusqu'à trois emprunts en cours.");
                }

                // Un seul exemplaire d'un livre parmi les emprunts en cours
                if (usager.Emprunts.Any(e => e.DateRetour == null && e.LivreID == emprunt.LivreID))
                {
                    throw new BadHttpRequestException($"Un seul exemplaire d'un livre peut être emprunté par " +
                        $"{usager.Prenom} {usager.Nom}.");
                }

                // La date de retour limite est obtenue en additionnant la date d’emprunt à un nombre
                // de jours obtenu à partir des fichiers de configuration de l’application API.
                // Ce nombre doit être défini à 10 jours dans le fichier appsettings.json
                emprunt.DateRetourLimite = emprunt.DateEmprunt.AddDays(_config.GetValue<int>("Emprunt:JourAlloue"));
            }


            // MàJ la quantité des livres (enregistrement dans la BDD pour réfleter la nouvelle quantité du livre)
            livre.Quantite--;
            _livresService.Modifier(livre);

            await _empruntsRepository.AddAsync(emprunt);
        }

        // Retourner un emprunt (rendre un livre)
        public async Task Modifier(Emprunt emprunt)
        {
            Emprunt empruntAModifie = await _empruntsRepository.GetByIdAsync(emprunt.Id);
            Livre livre = await _livresService.ObtenirSelonId(empruntAModifie.LivreID);
            Usager usager = await _usagersService.ObtenirSelonId(empruntAModifie.UsagerID);
            
            if(empruntAModifie.DateRetour != null)
            {
                throw new BadHttpRequestException($"L'emprunt est déjà retourné (livre rendu " +
                    $"le {empruntAModifie.DateRetour}).");
            }

            // Normalement, lors du retour d'un livre, la date sera la date d'aujourd'hui
            empruntAModifie.DateRetour = DateTime.Now;

            // Lors du retour du livre, une défaillance est automatiquement portée
            // au dossier de l’usager si la date de retour est supérieure
            // à la date limite de retour.
            if (empruntAModifie.DateRetour > empruntAModifie.DateRetourLimite)
            {
                usager.Defaillance++;
                
                // Mettre à jour le nombre de défaillance de l'usager
                _usagersService.Modifier(usager);
            }

            // MàJ la quantité des livres (enregistrement dans la BDD pour réfleter la nouvelle quantité du livre)
            livre.Quantite++;
            _livresService.Modifier(livre);

            await _empruntsRepository.EditAsync(empruntAModifie);
        }

        public Task<Emprunt> ObtenirSelonId(int id)
        {
            return _empruntsRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Emprunt>> ObtenirTout()
        {
            return _empruntsRepository.ListAsync();
        }

        public async Task Supprimer(Emprunt emprunt)
        {
            Livre livre = await _livresService.ObtenirSelonId(emprunt.LivreID);
            if (emprunt.DateRetour != null)
            {
                throw new BadHttpRequestException("L'emprunt ne peut être supprimé parce que le livre a été retourné.");
            }

            // Lors de la suppresion d'un emprunt, on doit incrémenter la quantité du livre correspondant
            livre.Quantite++;
            _livresService.Modifier(livre);
            
            await _empruntsRepository.DeleteAsync(emprunt);
        }
    }
}
