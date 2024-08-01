using LibraryApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Diagnostics;
using System.Linq;

namespace LibraryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Libarys() 
        {
            List<Libary> libarys = Data.Get.Libarys.ToList();
            return View(libarys);           
        }
        public IActionResult CreatLibary()
        {
            return View(new Libary());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreatLibary(Libary libary)
        {
            Data.Get.Libarys.Add(libary);
            Data.Get.SaveChanges();
            return RedirectToAction("Libarys"); 
        }
        //public IActionResult CreateShelf(int libId)
        //{
        //    Libary? Libary = Data.Get.Libarys.FirstOrDefault(libary => libary.Id == libId);
        //    Shelf shelf = new Shelf();
        //    shelf.Ganer = Libary;
        //    shelf.Height = " ";
        //    shelf.Name = " ";
        //    Data.Get.Shelfs.Add(shelf);
        //    //Data.Get.SaveChanges();
        //    return View(shelf);
        //}
        public IActionResult CreateShelves(int libId)
        {

            Shelf shelf = new Shelf();
            shelf.libId = libId;
            return View(shelf);
        }
        //[HttpPost, ValidateAntiForgeryToken]
        //public IActionResult CreateShelf(Shelf newShelf)
        //{
        //    Shelf? shelf = Data.Get.Shelfs.FirstOrDefault(Shelf => Shelf.Id == newShelf.Id);
        //    //שינוי הנתונים שהוחלפו
        //    Data.Get.Entry(shelf).CurrentValues.SetValues(newShelf);

        //    Data.Get.SaveChanges();

        //    return RedirectToAction("CreateShelf");
        //}
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateShelves(Shelf shelf)
        {
            Libary? Libary = Data.Get.Libarys.FirstOrDefault(libary => libary.Id == shelf.libId);
            shelf.Ganer = Libary;
            shelf.Id = 0;
            Shelf newSelf = new Shelf();
            newSelf.Ganer = Libary;
            newSelf.Name = shelf.Name;
            newSelf.Height = shelf.Height;
            newSelf.Id = shelf.Id;
            Data.Get.Shelfs.Add(newSelf);
            Data.Get.SaveChanges();
            return RedirectToAction("CreateShelves");
        }
        public IActionResult Books() 
        {
            List<Book> books = Data.Get.Books.Include(l => l.shelf).ToList(); 
            return View(books);
        }
   
        public IActionResult CreateBook()
        {
            Book book = new Book();
            return View(book);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateBook(Book book)
        {
            Shelf? shelf = Data.Get.Shelfs.Include(l => l.Ganer).ToList().FirstOrDefault(shelf => shelf.Name == book.StringShelf);
            if (shelf == null) { return NotFound(); }

            if (int.Parse(book.Height) > int.Parse(shelf.Height))
            {
                return RedirectToAction("hEror");
            }

            if (shelf.Ganer.Name.ToString() != book.Ganer.ToString())
            {
                return NotFound();
            }

            if (int.Parse(book.Height) <= (int.Parse(shelf.Height) - 10))
            {
                book.shelf = shelf;
                Data.Get.Books.Add(book);
                Data.Get.SaveChanges();
                return RedirectToAction("lEror");
            }

            if (shelf.Ganer.Name == book.Ganer)
            {
                book.shelf = shelf;
                Data.Get.Books.Add(book);
                Data.Get.SaveChanges();
                return RedirectToAction("Books");
            }

            return RedirectToAction("Books");
        }
        public IActionResult hEror()
        {
            return View();
        }
        public IActionResult lEror()
        {
            return View();
        }
        public IActionResult ChangeShelf(int id)
        {
            Book? book = Data.Get.Books.ToList().FirstOrDefault(book => book.Id == id);
            return View(book);
        }
        //הכרזה של הפונקציה הבאה לפוסט
        [HttpPost, ValidateAntiForgeryToken]
        //פונקציה לעידכון הפרטים
        public IActionResult ChangeShelf(Book newBook)
        {
            Book? book = Data.Get.Books.FirstOrDefault(book => book.Id == newBook.Id);
            //שינוי הנתונים שהוחלפו 
            Data.Get.Entry(book).CurrentValues.SetValues(newBook);
            //שמירת השינוים
            Data.Get.SaveChanges();
            return RedirectToAction("Books");
        }


    }
}
