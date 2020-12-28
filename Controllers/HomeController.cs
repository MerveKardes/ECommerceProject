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
using Microsoft.AspNetCore.Authorization;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
      private readonly SignInManager<AppUser> _signInManager;
      private readonly  IUrunRepository _urunRepository;
        private readonly ISepetRepository _sepetRepository;
        public HomeController(IUrunRepository urunRepository, SignInManager<AppUser> signInManager, ISepetRepository sepetRepository)
        {
            _sepetRepository = sepetRepository;
            _signInManager = signInManager;
            _urunRepository = urunRepository;
        }

        public IActionResult Index(int? kategoriId)
        {
            ViewBag.KategoriId = kategoriId;
            return View();
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

        public IActionResult Sepet()
        {
          
                return View(_sepetRepository.GetirSepettekiUrunler());
        }

        public IActionResult SepettenCikar(int id)
        {
            var cikarilacakUrun=_urunRepository.GetirIdile(id);
            _sepetRepository.SepettenCikar(cikarilacakUrun);
            return RedirectToAction("Sepet");
        }

        public IActionResult SepetiBosalt(decimal fiyat)
        {

            _sepetRepository.SepetiBosalt();
            return RedirectToAction("Tesekkur",new {fiyat=fiyat });
        }

        public IActionResult Tesekkur(decimal fiyat)
        {
            ViewBag.Fiyat = fiyat;
            return View();
        }
        [Authorize]
        public IActionResult EkleSepet(int id)
        {
            var urun=_urunRepository.GetirIdile(id);
            _sepetRepository.SepeteEkle(urun);
            TempData["bildirim"] = "Ürün sepete eklendi";
            return RedirectToAction("Index");
        }
    }
}
