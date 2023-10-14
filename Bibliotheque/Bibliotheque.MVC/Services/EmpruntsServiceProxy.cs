using Bibliotheque.MVC.Interfaces;
using Bibliotheque.MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace Bibliotheque.MVC.Services
{
    public class EmpruntsServiceProxy : IEmpruntsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _empruntsApiUrl = "api/emprunts/";

        public EmpruntsServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Ajouter(RequeteEmprunt requeteEmprunt)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_empruntsApiUrl, requeteEmprunt);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = response.Content.ReadAsStringAsync().Result;
                throw new HttpRequestException(errorMessage);
            }
        }

        public async Task Modifier(Emprunt item)
        {
            // Constuire la requete avec les données au format JSON
            StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(_empruntsApiUrl + item.ID, content);
        }

        public async Task<Emprunt> ObtenirSelonId(int id)
        {
            return await _httpClient.GetFromJsonAsync<Emprunt>(_empruntsApiUrl + id);
        }

        public async Task<IEnumerable<Emprunt>> ObtenirTout()
        {
            var response = await _httpClient.GetAsync(_empruntsApiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erreur lors de la récupération des emprunts");
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<Emprunt>>();
        }

        public async Task Supprimer(Emprunt item)
        {
            var response = await _httpClient.DeleteAsync(_empruntsApiUrl + item.ID);

            if(!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Un emprunt ne peut être supprimé puisque le livre est retourné");
            }
        }
    }
}
