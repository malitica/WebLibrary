using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBiblioteka.Models
{
    [Table("Komentar")]
    public class Komentar
    {
        [Key]
        public int KomentarId { get; set; }
        public string Autor { get; set; }
        [Display(Name = "Naslov komentara")]
        [Required(ErrorMessage = "Unesite naslov komentara")]
        [StringLength(30, ErrorMessage = "Najmanje 5 a navise 30 karaktera", MinimumLength = 5)]
        public string Naslov { get; set; }
        [Display(Name = "Komentar")]
        [Required(ErrorMessage = "Unesite komentar")]
        [StringLength(100, ErrorMessage = "Najmanje 10 a najvise 100 karaktera", MinimumLength = 10)]
        public string SadrzajKomentara { get; set; }
    }

}
