using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Shelf
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="שם מדף")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "גובה המדף")]
        public string Height { get; set; }= string.Empty;
        [Display(Name = "ז'אנר")]
        public Libary Ganer { get; set; }
        [Display(Name = "מדף"), NotMapped]
        public int libId { get; set; }
        
    }
}
