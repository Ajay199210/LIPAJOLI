using System.ComponentModel.DataAnnotations;

namespace Bibliotheque.MVC.Models
{
    public class Emprunt
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Le champ est obligatoire")]
        [Display(Name = "ID de l'usager")]
        public int UsagerID { get; set; }

        [Required(ErrorMessage = "Le champ est obligatoire")]
        [Display(Name = "ID du livre")]
        public int LivreID { get; set; }

        [Required(ErrorMessage = "Le champ est obligatoire")]  // égal au DateTime.Today()
        [Display(Name = "Date d'emprunt")]
        [DataType(DataType.Date)]
        public DateTime DateEmprunt { get; set; }

        [Display(Name = "Date de retour attendu")]
        [DataType(DataType.Date)]
        public DateTime DateRetourLimite { get; set; } // égal à DateEmprunt + NbJourIndiqueDansFichierConfiguration calculé en lien au temps autorisé pour l'emprunt

        [Display(Name = "Date de retour")]
        [DataType(DataType.Date)]
        public DateTime? DateRetour { get; set; } // Sera absente à l'instanciation car elle correspond à la date de retour du livre par l'usager


        // Propriétés de navigation 
        public Livre? Livre { get; set; }
        public Usager? Usager { get; set; }
    }
}
