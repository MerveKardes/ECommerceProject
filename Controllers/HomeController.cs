using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_Ticaret.Models;
using E_Ticaret.Interfaces;
using Microsoft.AspNetCore.Identity;
using E_Ticaret.Entities;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
      private readonly SignInManager<AppUser> _signInManager;
      private readonly  IUrunRepository _urunRepository;
        public HomeController(IUrunRepository urunRepository, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
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
               var signInResult= _signInManager.PasswordSignInAsync(model.KullaniciAd, model.Sifre, model.BeniHatirla, false).Result;
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            }
            return View(new KullaniciGirisModel());
        }
    }
}
