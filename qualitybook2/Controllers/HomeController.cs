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
    public class HomeController : Controller
    {
        private readonly  IBookRepository _iBookRepository;

        public HomeController(IBookRepository iBookRepository)
        {
            _iBookRepository = iBookRepository;
        }

        // GET: PreferredBooks
        public  ViewResult Index()
        {
            var vm = new PreferredBookListViewModel();
            vm.PreferredBooks = _iBookRepository.PreferredBooks;
            return View(vm);
        }
    }
}
