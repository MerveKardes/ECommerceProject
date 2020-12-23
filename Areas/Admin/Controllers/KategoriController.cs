using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Ticaret.Entities;
using E_Ticaret.Interfaces;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KategoriController : Controller
    {
        private readonly IKategoriRepository _kategoriRepository;
        public KategoriController(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }
        public IActionResult Index()
        {
            return View(_kategoriRepository.GetirHepsi());
        }

        public IActionResult Ekle()
        {
            return View(new KategoriEkleModel());
        }

        [HttpPost]
        public IActionResult Ekle(KategoriEkleModel model)
        {
            if (ModelState.IsValid)
            {
                _kategoriRepository.Ekle(new Kategori
                {
                    Ad = model.Ad
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
