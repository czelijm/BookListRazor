using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //Create
            if (id==null)
            {
                Book = new Book();
                return Page();
            }

            //Update
            //Book = await _db.Books.FindAsync(id); //it's ok too
            Book = await _db.Books.FirstOrDefaultAsync( b => b.Id == id);

            // return Book==null? NotFound() : Page(); //not in c# 8.0
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //updating better for few properties
                //var bookFromDb = await _db.Books.FindAsync(Book.Id);
                //bookFromDb.Name = Book.Name;
                //bookFromDb.Author = Book.Author;
                //bookFromDb.ISBN = Book.ISBN;
                if (Book.Id==0)
                {
                    await _db.Books.AddAsync(Book);
                }
                else
                {
                    //update better for entire obcject
                    _db.Books.Update(Book);
                }


                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }



    }
}
