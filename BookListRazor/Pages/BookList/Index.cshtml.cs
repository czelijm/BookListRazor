using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookListRazor.Models;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await _db.Books.ToListAsync();
        }

        //OnPost becouse is triggerd from button in index.cshtml 
        public async Task<IActionResult> OnPostDelete(int id) {

            var bookFromDb = await _db.Books.FindAsync(id);
            if ((bookFromDb is null || bookFromDb == null))
            {
                return NotFound();
            }

            _db.Books.Remove(bookFromDb);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}
