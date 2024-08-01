using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "שם ספר")]
        public string Name { get; set; }

        [Display(Name = "גובה הספר")]
        public string Height { get; set; }
        [Display(Name = "ז'אנר")]
        public string Ganer { get; set; }

        [Display(Name = "מדף"), NotMapped]
        public string StringShelf { get; set; }
        public Shelf shelf { get; set; }

    }   
}
