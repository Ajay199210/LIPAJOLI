using Bibliotheque.MVC.Interfaces;
using Bibliotheque.MVC.Models;

namespace Bibliotheque.MVC.Services
{
    public class LivresServiceProxy : ILivresService
    {
        private readonly HttpClient _httpClient;
        private readonly string _livresApiUrl = "api/livres/";

        public LivresServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Ajouter(Livre livre)
        {
            var response = await _httpClient.PostAsJsonAsync(_livresApiUrl, livre);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erreur lors de l'ajout d'un livre");
            }
        }

        public Task Modifier(Livre item)
        {
            throw new NotImplementedException();
        }

        public Task<Livre> ObtenirSelonId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Livre>> ObtenirTout()
        {
            var response = await _httpClient.GetAsync(_livresApiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erreur lors de la récupération des livres");
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<Livre>>();
        }

        public Task Supprimer(Livre item)
        {
            throw new NotImplementedException();
        }
    }
}
