using Bibliotheque.MVC.Models;

namespace Bibliotheque.MVC.Data
{
    public class DbInitializer
    {
        public static void Initialize(BibliothequeContext context)
        {
            context.Database.EnsureCreated();

            if (context.Usagers.Any())
            {
                return;   // DB has been seeded
            }

            // Génération des usagers
            var usagers = new Usager[]
            {
            new Usager{ ID = 1, Courriel="usager1@def.com", Prenom="Bob", Nom="Bricoleur", Statut=Statut.Enseignant, No=123456},
            new Usager{ ID = 2, Courriel="usager2@domaine.ca", Prenom="Dora", Nom="Exploratrice", Statut=Statut.Étudiant, No=098765},
            new Usager{ ID = 3, Courriel="usager3@exemple.ca", Prenom="Jhon", Nom="Lecteur", Statut=Statut.Étudiant, No=543210}
            };

            foreach (Usager u in usagers)
            {
                context.Usagers.Add(u);
            }

            context.SaveChanges();

            // Génération des livres
            var livres = new Livre[]
            {
            new Livre{Titre="Anna Karenina", Auteurs="Tolstoy",Categorie="Fiction", CodeUnique="FIC004",
                Isbn10="0393966429", Isbn13="9780393966428", Prix=10.99, Quantite=2},

            new Livre{Titre="L'école des femmes", Auteurs="Molière",Categorie="Fiction", CodeUnique="FIC003",
                Isbn10="0151795800", Isbn13="9780151795802", Prix=6.99, Quantite=6},

            new Livre{Titre="Titre X", Auteurs="Tolstoy" + "," + " " + "Molière",Categorie="Fiction",
                CodeUnique="FIC001", Isbn10="3770121880", Isbn13="9783770121885", Prix=0.99, Quantite=666},

            new Livre{Titre="Guerre et Paix", Auteurs="Tolstoy",Categorie="Fiction", CodeUnique="FIC002",
                Isbn10="8804682590", Isbn13="9788804682592", Prix=12.99, Quantite=4},

            new Livre{Titre="Titre Y", Auteurs="Goethe",Categorie="Romany", CodeUnique="ROM001",
                Isbn10="3175046590", Isbn13="9783173042510", Prix=12.99, Quantite=7}
            };
            
            foreach (Livre l in livres)
            {
                context.Livres.Add(l);
            }

            context.SaveChanges();

            // Génération des emprunts
            var emprunts = new Emprunt[]
            {
            new Emprunt{LivreID=1, UsagerID=1, DateEmprunt=DateTime.Now, DateRetourLimite=DateTime.Now.AddDays(10),
                DateRetour=DateTime.Now.AddDays(3)},

            new Emprunt{LivreID=1, UsagerID=2, DateEmprunt=DateTime.Now, DateRetourLimite=DateTime.Now.AddDays(10),
                DateRetour=DateTime.Now.AddDays(20)},

            new Emprunt{LivreID=2, UsagerID=1, DateEmprunt=DateTime.Now.AddDays(-5), DateRetourLimite=DateTime.Now.AddDays(5),
                DateRetour=null},
            
            new Emprunt{LivreID=3, UsagerID=2, DateEmprunt=DateTime.Now.AddDays(-3), DateRetourLimite=DateTime.Now.AddDays(7),
                DateRetour=DateTime.Now.AddDays(3)},

            new Emprunt{LivreID=4, UsagerID=2, DateEmprunt=DateTime.Now.AddDays(-10), DateRetourLimite=DateTime.Now,
                DateRetour=DateTime.Now.AddDays(15)},

            new Emprunt{LivreID=5, UsagerID=3, DateEmprunt=DateTime.Now, DateRetourLimite=DateTime.Now.AddDays(10),
                DateRetour=null},

            new Emprunt{LivreID=2, UsagerID=3, DateEmprunt=DateTime.Now.AddDays(-4), DateRetourLimite=DateTime.Now.AddDays(6),
                DateRetour=DateTime.Now.AddDays(10)},
            };

            foreach (Emprunt e in emprunts)
            {
                context.Emprunts.Add(e);
            }

            context.SaveChanges();
        }
    }
}
