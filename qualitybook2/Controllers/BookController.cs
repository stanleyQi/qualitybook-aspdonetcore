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
        public  ViewResult Search([FromQuery(Name = "searchKey")] string searchKey="", 
                                                    [FromQuery(Name = "searchCriteria")] int searchCriteria = 1, 
                                                    [FromQuery(Name = "categoryId")] int categoryId = 0)
        {
            var vm = new BookListViewModel();
            vm.Categories = _iCategoryRepository.Categories;
            vm.Books = _iBookRepository.SearchedBooks(searchKey, searchCriteria, categoryId);
            return View("Views/Book/Index.cshtml",vm);
        }
    }
}
