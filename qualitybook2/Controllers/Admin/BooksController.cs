using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qualitybook2.Data;
using qualitybook2.Models;

namespace qualitybook2.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class BooksController : Controller
    {
        private readonly QualityBookDbContext _context;
        private string ViewPath = "Views/Admin/Books/";

        public BooksController(QualityBookDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var BookList = _context.Books.Include(b => b.Category).Include(b => b.Supplier);
            return View(ViewPath + "Index.cshtml", await BookList.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Supplier)
                .SingleOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(ViewPath + "Details.cshtml", book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["Supplier"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
            return View(ViewPath + "Create.cshtml");
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,BookName,ShortDescription,Description,ImageUrl,Price,Author,PreferredFlag,CategoryId,SupplierId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            ViewData["Supplier"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", book.SupplierId);
            return View(ViewPath + "Create.cshtml", book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            ViewData["Supplier"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", book.SupplierId);
            return View(ViewPath + "Edit.cshtml", book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int BookId, [Bind("BookId,BookName,ShortDescription,Description,ImageUrl,Price,Author,PreferredFlag,CategoryId,SupplierId")] Book book)
        {
            if (BookId != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            ViewData["Supplier"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", book.SupplierId);
            return View(ViewPath + "Edit.cshtml", book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Supplier)
                .SingleOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(ViewPath + "Delete.cshtml", book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int BookId)
        {
            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookId == BookId);
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
