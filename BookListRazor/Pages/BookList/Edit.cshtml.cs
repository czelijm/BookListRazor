using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book {get; set;}

        public async Task OnGetAsync(int id)
        {
            Book = await _db.Books.FindAsync(id); //((b) => b.Id == id).Add
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var bookFromDb = await _db.Books.FindAsync(Book.Id);
                bookFromDb.Name = Book.Name;
                bookFromDb.Author = Book.Author;
                bookFromDb.ISBN = Book.ISBN;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }

        //public async Task OnUpdateAsync() 
        //{

        //}

    }
}
