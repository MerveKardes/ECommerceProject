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
      private readonly  IUrunRepository _urunRepository;
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

           public IActionResult GirisYap()
            {
            return View(new KullaniciGirisModel());
            }

        [HttpPost]
        public IActionResult GirisYap(KullaniciGirisModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(new KullaniciGirisModel());
        }
    }
}
