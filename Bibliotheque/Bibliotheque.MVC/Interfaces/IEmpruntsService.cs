using Bibliotheque.MVC.Models;

namespace Bibliotheque.MVC.Interfaces
{
    public interface IEmpruntsService
    {
        public Task Ajouter(RequeteEmprunt requeteEmprunt);
        public Task<Emprunt> ObtenirSelonId(int id);
        public Task<IEnumerable<Emprunt>> ObtenirTout();
        public Task Modifier(Emprunt item);
        public Task Supprimer(Emprunt item);
    }
}
