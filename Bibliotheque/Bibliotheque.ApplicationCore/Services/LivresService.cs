using Bibliotheque.ApplicationCore.Entites;
using Bibliotheque.ApplicationCore.Interfaces;

namespace Bibliotheque.ApplicationCore.Services
{
    public class LivresService : ILivresService
    {
        private readonly IAsyncRepository<Livre> _livresRepository;
        public LivresService(IAsyncRepository<Livre> livresRepository)
        {
            _livresRepository = livresRepository;
        }

        public Task Ajouter(Livre item)
        {
            return _livresRepository.AddAsync(item);
        }

        public Task Modifier(Livre item)
        {
            return _livresRepository.EditAsync(item);
        }

        public Task<Livre> ObtenirSelonId(int id)
        {
            return _livresRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Livre>> ObtenirTout()
        {
            return _livresRepository.ListAsync();
        }

        public Task Supprimer(Livre item)
        {
            return _livresRepository.DeleteAsync(item);
        }
    }
}
