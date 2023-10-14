using Bibliotheque.ApplicationCore.Entites;

namespace Bibliotheque.ApplicationCore.Interfaces
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
