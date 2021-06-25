using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBiblioteka.Models;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using System.Collections;


namespace WebBiblioteka.Controllers
{
    public class BookController : Controller
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }


       
        // GET: Books
        public async Task<IActionResult> Index(int pageNumber=1)
        {
            TempData["Test"] = "Index";
            return View(await PaginatedList<Book>.CreateAsync(_context.Books,pageNumber,8));
        }

        public PartialViewResult _TraziKnjige(string deoNaslova, int id = 0)
        {
            IEnumerable<Book> listaKnjiga = _context.Books;
          
            if (!string.IsNullOrWhiteSpace(deoNaslova))
            {
                listaKnjiga = listaKnjiga
                .Where(b => b.Title.ToLower().Contains(deoNaslova.ToLower()));
            }
           
            return PartialView(listaKnjiga);
        }


        public FileContentResult ReadImg(int? id)
        {
            if (id == null)
            {
                return null;
            }
            Book bk = _context.Books.Find(id);
            if (id == null)
            {
                return null;
            }
            return File(bk.Picture, bk.TypeOfFile);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("BookId,Title,NumOfPages,Picture,TypeOfFile,Author,BookDesc")] Book book, IFormFile SelectedPic)
        {
            if (SelectedPic == null)
            {
                ModelState.AddModelError("BinaryData", "You did not select any image");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await SelectedPic.CopyToAsync(ms);
                        book.Picture = ms.ToArray();
                    }
                    book.TypeOfFile = SelectedPic.ContentType;
                    book.IsApproved = false;
                    _context.Add(book);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Success";
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception)
                {
                    ViewBag.Greska = "Error occured while trying to save the book";
                }

            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int BookId, string Title, int NumOfPages, string Author, string BookDesc, IFormFile SelectedPic)
        {
         
            if (id != BookId)
            {
                return NotFound();
            }
            Book book = await _context.Books.FirstOrDefaultAsync(test => test.BookId == BookId);
            
          
            if (ModelState.IsValid)
            {
               
                try
                {
                    if (SelectedPic != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await SelectedPic.CopyToAsync(ms);
                            book.Picture = ms.ToArray();
                            book.TypeOfFile = SelectedPic.ContentType;
                            book.IsApproved = false;
                            book.Title = Title;
                            book.NumOfPages = NumOfPages;
                            book.Author = Author;
                            book.BookDesc = BookDesc;
                        }
                    }
                    else
                    {
                        book.Title = Title;
                        book.NumOfPages = NumOfPages;
                        book.Author = Author;
                        book.BookDesc = BookDesc;
                        book.IsApproved = false;
                    }

                   
                    _context.Update(book);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessEdit"] = "Success";
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [Authorize(Policy = "AdminOnly")]
        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            
            return View(book);
        }
        // GET: Books/Delete/5
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Policy = "AdminOnly")]
        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
