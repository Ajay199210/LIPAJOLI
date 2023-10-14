using Bibliotheque.MVC.Data;
using Bibliotheque.MVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bibliotheque.MVC.Services
{
    public class GenerateurCode : IGenerateurCode
    {
        private readonly BibliothequeContext _context;

        public GenerateurCode(BibliothequeContext context)
        {
            _context = context;
        }

        public async Task<string> GenererCode(string categorie)
        {
            var livresCategorie = await _context.Livres.Where(l => l.Categorie == categorie).ToListAsync();

            string strCategorie = categorie.Substring(0, 3).ToUpper(); // Code catégorie
            int numLivresCategorie = 1;
            if (livresCategorie.Any())
            {
                numLivresCategorie = livresCategorie.Max(l => int.Parse(l.CodeUnique.Substring(3, 3))) + 1;
            }
            return strCategorie + numLivresCategorie.ToString("D3");
        }
    }
}
