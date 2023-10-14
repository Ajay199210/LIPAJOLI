using Bibliotheque.ApplicationCore.Entites;
using Bibliotheque.ApplicationCore.Interfaces;

namespace Bibliotheque.ApplicationCore.Services
{
    public class UsagersService : IUsagersService
    {
        private readonly IAsyncRepository<Usager> _usagersRepository;
        public UsagersService(IAsyncRepository<Usager> usagersRepository)
        {
            _usagersRepository = usagersRepository;
        }

        public Task Ajouter(Usager item)
        {
            return _usagersRepository.AddAsync(item);
        }

        public Task Modifier(Usager item)
        {
            return _usagersRepository.EditAsync(item);
        }

        public Task<Usager> ObtenirSelonId(int id)
        {
            return _usagersRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Usager>> ObtenirTout()
        {
            return _usagersRepository.ListAsync();
        }

        public Task Supprimer(Usager item)
        {
            return _usagersRepository.DeleteAsync(item);
        }
    }
}
