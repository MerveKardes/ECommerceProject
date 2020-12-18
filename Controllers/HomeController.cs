using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_Ticaret.Models;
using E_Ticaret.Interfaces;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        IUrunRepository _urunRepository;
        public HomeController(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }

        public IActionResult Index()
        {
            return View(_urunRepository.GetirHepsi());
        }

      public IActionResult UrunDetay(int id)
        {
            return View(_urunRepository.GetirIdile(id));
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
