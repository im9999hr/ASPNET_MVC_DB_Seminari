using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SeminarUpisi.Models.Validacije;

namespace SeminarUpisi.Models
{
    public class Seminar
    {
        [Key]
        public int IdSeminar { get; set; }
        [Required(ErrorMessage = "Naziv seminara je obvezan!")]
        [StringLength(100, ErrorMessage = "Naziv seminara ne može imati više od 100 znakova!")]
        public string Naziv { get; set; }
        [UIHint("MultilineText")]
        public string Opis { get; set; }
        [Required(ErrorMessage = "Datum početka seminara je obvezan!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [SeminarNeUProslosti(ErrorMessage = "Seminar ne može počinjati u prošlosti!")]
        public DateTime Datum { get; set; }
        [StringLength(50, ErrorMessage = "Ime i prezime predavača skupa ne može imati više od 50 znakova!")]
        [Display(Name ="Predavač")]
        public string Predavac { get; set; }
        [Required]
        public bool Popunjen { get; set; }
        public List<Predbiljezba> Predbiljezbe { get; set; }

        public Seminar()
        {
            Predbiljezbe = new List<Predbiljezba>();
        }
    }
}