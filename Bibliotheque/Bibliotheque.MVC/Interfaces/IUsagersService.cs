using Bibliotheque.MVC.Models;

namespace Bibliotheque.MVC.Interfaces
{
    public interface IUsagersService
    {
        public Task Ajouter(Usager item);
        public Task<Usager> ObtenirSelonId(int id);
        public Task<IEnumerable<Usager>> ObtenirTout();
        public Task Modifier(Usager item);
        public Task Supprimer(Usager item);
    }
}
