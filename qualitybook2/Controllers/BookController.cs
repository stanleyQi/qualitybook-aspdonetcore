using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qualitybook2.Data;
using qualitybook2.Data.interfaces;
using qualitybook2.Models;
using qualitybook2.tools;
using qualitybook2.ViewModels;

namespace qualitybook2.Controllers
{
    public class BookController : Controller
    {
        private readonly  IBookRepository _iBookRepository;
        private readonly ICategoryRepository _iCategoryRepository;

        public BookController(IBookRepository iBookRepository, ICategoryRepository iCategoryRepository)
        {
            _iBookRepository = iBookRepository;
            _iCategoryRepository = iCategoryRepository;
        }

        // GET: Search Books by keyword and criteria
        public async Task<ViewResult> Search([FromQuery(Name = "searchKey")] string searchKey="", 
                                [FromQuery(Name = "searchCriteria")] int searchCriteria = 1, 
                                [FromQuery(Name = "categoryId")] int categoryId = 0,
                                string sortOrder = "ByName",
                                int pageNumber =1
                                )
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["categoryId"] = categoryId;

            var vm = new BookListViewModel();
            vm.Categories = _iCategoryRepository.Categories;
            //vm.Books = _iBookRepository.SearchedBooks(searchKey, searchCriteria, categoryId);

            //return View("Views/Book/Index.cshtml",vm);

            var books = _iBookRepository.SearchedBooks(searchKey, searchCriteria, categoryId);
            switch (sortOrder)
            {
                case "ByName":
                    books = books.OrderBy(s => s.BookName);
                    break;
                case "ByPrice":
                    books = books.OrderBy(s => s.Price);
                    break;
                default:
                    books = books.OrderBy(s => s.BookName);
                    break;
            }
            int pageSize = 3;
            var paginatedList = await PaginatedList<Book>.CreateAsync(books, pageNumber,pageSize);
            vm.Books = paginatedList.Items;
            vm.HasPreviousPage = paginatedList.HasPreviousPage;
            vm.HasNextPage = paginatedList.HasNextPage;
            vm.PageIndex = paginatedList.PageIndex;
            return View("Views/Book/Index.cshtml", vm);
        }
    }
}
