using Book.Models;
using Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Controllers
{
    public class BookController : Controller
    {
        List<BookViewModel> bookViewModel =  new List<BookViewModel>() {
                new BookViewModel
                {
                    BookId = 1,
                    BookName = "Momo",
                    Author = "Michael Ende",
                    PublishedYear = 1973
                },
                new BookViewModel
               {
                  BookId=2,
                  BookName = "1984",
                  Author="George Orwell",
                  PublishedYear=1949
               },
                new BookViewModel
               {
                  BookId=3,
                  BookName = "Hayvan Çiftliği",
                  Author="George Orwell",
                  PublishedYear=1945
               },
                new BookViewModel
               {
                  BookId=4,
                  BookName = "Kürk Mantolo Madonna",
                  Author="Sabahattin Ali",
                  PublishedYear=1943
               },
                new BookViewModel
               {
                   BookId=5,
                  BookName = "Hayvanlardan Destek Alma Sanatı",
                  Author="Çetin Çetintaş",
                  PublishedYear=2021
               }
            };

        public BookController()
        {
           
        }
        public IActionResult Index()
        {
           if (HttpContext.Session.Get<List<BookViewModel>> ("book") == default)
            {
                HttpContext.Session.Set<List<BookViewModel>>("book", bookViewModel);
            }
            return RedirectToAction("GetAllBooks");
        }


        public IActionResult GetAllBooks()
        {
            List<BookViewModel> model = null;
            if (HttpContext.Session.Get<List<BookViewModel>>("book") != default)
            {
                 model = HttpContext.Session.Get<List<BookViewModel>>("book");
            }
             
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
       public IActionResult Add(BookViewModel book)
        {
            bookViewModel.Add(book);
            if ((HttpContext.Session.Get<List<BookViewModel>>("books") == default))
            {
                HttpContext.Session.Set<List<BookViewModel>> ("books", bookViewModel);
            }
            return View("GetAllBooks",bookViewModel);
        }

        public IActionResult Favorite(string bookName)
        {
            Response.Cookies.Append("bookName", bookName);
            string cookie = string.Empty;
            if (Request.Cookies.ContainsKey("bookName"))
                cookie = Request.Cookies["bookName"];
            ViewBag.cookie = cookie;
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int bookId)
        {
            var bookToDelete =  bookViewModel.Where(b => b.BookId == bookId).FirstOrDefault();
            bookViewModel.Remove(bookToDelete);
            return RedirectToAction("Index",bookViewModel);
        }

      
    }
}
