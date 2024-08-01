using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;


namespace LibraryApp.Models
{
    public class DataLayer : DbContext
    {
        //שליחה לקונסטרקטור האבא את החיבור למסד
        public DataLayer(string connectionString) : base(GetOptions(connectionString))
        {
            //וידוא שיוצר המסד
            Database.EnsureCreated();
            //ערך ראשוני דיפולטיבי
            Seed();
        }

        private void Seed()
        {
            SeedBook();
        }

        private void SeedBook()
        {
            if (Books.Any())
            {
                return ;
            }
            Book book = new Book();
            book.Height = "25";
            book.StringShelf = "מדף 1";
            book.Name = "אורחות צדיקים";
            book.Ganer = "מוסר";
            book.shelf = SeedShelf();
            Books.Add(book);
            SaveChanges();
        }
        private Shelf SeedShelf()
        {
            if (Shelfs.Any()) { return Shelfs.First(); }
            else {
                Shelf shelf = new Shelf();
                shelf.Name = "מדף 1";
                shelf.Height = "30";
                shelf.Ganer = SeedLibary();
                Shelfs.Add(shelf);
                SaveChanges(); return shelf;    
            }

        }
        private Libary SeedLibary()
        {
            if (Libarys.Any()) { return Libarys.First(); }
            else {
                Libary libary = new Libary();
                libary.NameLibary = "ספריה 1";
                libary.Name = "מוסר";
                Libarys.Add(libary);
                SaveChanges();return libary;
            }
        }

        public DbSet<Libary> Libarys { get; set; }

        public DbSet<Shelf> Shelfs { get; set; }
        public DbSet<Book> Books { get; set; }
        //פונקציה סטטית שמחזירה את אופצית החיבור למסד
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions
                  .UseSqlServer(new DbContextOptionsBuilder(), connectionString)
                  .Options;
        }
    }
}
