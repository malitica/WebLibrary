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
        [Display(Name = "Author")]
        public string Autor { get; set; }
        [Display(Name = "Headline")]
        [Required(ErrorMessage = "Please enter headline")]
        [StringLength(30, ErrorMessage = "Minimum 5 and maximum 30 characters", MinimumLength = 5)]
        public string Naslov { get; set; }
        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Please enter comment")]
        [StringLength(100, ErrorMessage = "Minimum 10 and maximum 100 characters", MinimumLength = 10)]
        public string SadrzajKomentara { get; set; }
    }

}
