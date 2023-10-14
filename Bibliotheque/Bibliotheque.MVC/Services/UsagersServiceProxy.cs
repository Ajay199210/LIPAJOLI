using Bibliotheque.MVC.Interfaces;
using Bibliotheque.MVC.Models;

namespace Bibliotheque.MVC.Services
{
    public class UsagersServiceProxy : IUsagersService
    {
        private readonly HttpClient _httpClient;
        private readonly string _usagersApiUrl = "api/usagers/";

        public UsagersServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Ajouter(Usager usager)
        {
            var response = await _httpClient.PostAsJsonAsync(_usagersApiUrl, usager);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erreur lors de l'ajout de l'usager");
            }
        }

        public Task Modifier(Usager usager)
        {
            throw new NotImplementedException();
        }

        public Task<Usager> ObtenirSelonId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Usager>> ObtenirTout()
        {
            var response = await _httpClient.GetAsync(_usagersApiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erreur lors de la récupération des usagers");
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<Usager>>();
        }

        public Task Supprimer(Usager item)
        {
            throw new NotImplementedException();
        }
    }
}
