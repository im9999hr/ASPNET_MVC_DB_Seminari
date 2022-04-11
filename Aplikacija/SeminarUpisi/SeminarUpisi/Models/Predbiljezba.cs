using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeminarUpisi.Models
{
    public class Predbiljezba
    {
        [Key]
        public int IdPredbiljezba { get; set; }
        [Required(ErrorMessage ="Datum predbilježbe je obvezan!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }
        [Required(ErrorMessage = "Ime je obvezno!")]
        [StringLength(25, ErrorMessage = "Ime ne može imati više od 25 znakova!")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obvezno!")]
        [StringLength(25, ErrorMessage = "Prezime ne može imati više od 25 znakova!")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Adresa je obvezna!")]
        [StringLength(100, ErrorMessage = "Adresa ne može imati više od 100 znakova!")]
        public string Adresa { get; set; }
        [Required(ErrorMessage = "Email je obvezan!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Broj telefona je obvezan!")]
        [StringLength(30, MinimumLength =5, ErrorMessage ="Broj znamenki mora biti unutar 5 i 30 znakova!")]
        public string Telefon { get; set; }
        public int IdSeminar { get; set; }
        public Seminar Seminar { get; set; }
        [StringLength(25)]
        [UIHint("TemplStatus")]
        public string Status { get; set; }
    }

    public enum StatusPredbiljezbe
    {
        Neobradjena,
        Prihvacena,
        Odbijena
    }
}