using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookListRazor.Models;

namespace BookListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        //IEnumerable<Book> Books { get; set; }

        private readonly ApplicationDbContext _db;

        //On Exchange OnPost(Book book) use decorator [bindProperty] - that assumes that this on post you will get this book
        [BindProperty]
        public Book Book { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() 
        {
            if (ModelState.IsValid)
            {
                await _db.Books.AddAsync(Book);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
            
        }
    }
}
