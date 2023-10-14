using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotheque.ApplicationCore.Entites
{
    public class Emprunt : BaseEntity
    {
        [Display(Name = "Usager"), Required(ErrorMessage = "Veuillez sélectionner un usager")]
        [ForeignKey("Usagers"), Column(Order = 0)]
        public int UsagerID { get; set; }

        [Display(Name = "Livre"), Required(ErrorMessage = "Veuillez sélectionner un livre")]
        [ForeignKey("Livres"), Column(Order = 1)]
        public int LivreID { get; set; }

        [Required(ErrorMessage = "Le champ est obligatoire")]  // AVF TP3 égal au DateTime.Today()
        [Display(Name = "Date d'emprunt")]
        [DataType(DataType.Date)]
        public DateTime DateEmprunt { get; set; }

        [Display(Name = "Date de retour attendu")]
        [DataType(DataType.Date)]
        public DateTime DateRetourLimite { get; set; } // AVF TP3 égal à DateEmprunt + NbJourIndiqueDansFichierConfiguration calculé en lien au temps autorisé pour l'emprunt

        [Display(Name = "Date de retour")]
        [DataType(DataType.Date)]
        public DateTime? DateRetour { get; set; } // Sera absente à l'instanciation car elle correspond a la date de retour du livre par l'Usager

        // Propriétés de navigation
        public virtual Livre? Livre { get; set; }
        public virtual Usager? Usager { get; set; }
    }
}
