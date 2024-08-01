using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Libary
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "שם סיפריה")]
        public string NameLibary { get; set; }
        [Display(Name = "שם הז'אנר")]
        public string Name { get; set; }
    }
}
