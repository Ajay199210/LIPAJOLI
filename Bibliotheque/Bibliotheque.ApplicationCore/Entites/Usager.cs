using Bibliotheque.MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bibliotheque.ApplicationCore.Entites
{
    public class Usager : BaseEntity
    {
        //[DisplayFormat(DataFormatString = "{0:D10}")]
        public int No { get; set; }

        [Required(ErrorMessage = "Le champ est obligatoire")]
        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "La taille maximale du champ est de 50 caractères")]
        public string? Nom { get; set; }

        [Required(ErrorMessage = "Le champ est obligatoire")]
        [DataType(DataType.Text)]
        [Display(Name = "Prénom")]
        [MaxLength(50, ErrorMessage = "La taille maximale du champ est de 50 caractères")]
        public string? Prenom { get; set; }

        [DisplayFormat(NullDisplayText = "Choisir un statut")]
        [Required(ErrorMessage = "Le champ est obligatoire")]
        public Statut Statut { get; set; }

        [Display(Name = "Nombre de défaillances")]
        public int Defaillance { get; set; } = 0; // Initialisée à zéro 

        [DataType(DataType.EmailAddress)]
        public string? Courriel { get; set; }

        // N'affiche pas la propriété de navigation
        [JsonIgnore]
        public virtual ICollection<Emprunt>? Emprunts { get; set; }
    }
}
