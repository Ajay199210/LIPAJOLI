using Bibliotheque.MVC.Models;

namespace Bibliotheque.MVC.Interfaces
{
    public interface ILivresService
    {
        public Task Ajouter(Livre item);
        public Task<Livre> ObtenirSelonId(int id);
        public Task<IEnumerable<Livre>> ObtenirTout();
        public Task Modifier(Livre item);
        public Task Supprimer(Livre item);
    }
}
