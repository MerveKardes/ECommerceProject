using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using E_Ticaret.Entities;
using E_Ticaret.Interfaces;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
         private readonly IUrunRepository _urunRepository;
        private readonly IKategoriRepository _kategoriRepository;

        public HomeController(IUrunRepository urunRepository, IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
            _urunRepository = urunRepository;
        }


        public IActionResult Index()
        {
            return View(_urunRepository.GetirHepsi());
        }

        public IActionResult Ekle()
        {
            return View(new UrunEkleModel());
        }

        [HttpPost]
        public IActionResult Ekle(UrunEkleModel model)
        {
            Urun urun = new Urun();
            if (ModelState.IsValid)
            {
                if (model.Resim != null)
                {
                    var uzanti = Path.GetExtension(model.Resim.FileName);
                    var yeniResimAd = Guid.NewGuid() + uzanti;
                    var yuklenecekYer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/"+yeniResimAd);

                    var stream = new FileStream(yuklenecekYer, FileMode.Create);
                    model.Resim.CopyTo(stream);

                    urun.Resim = yeniResimAd;
                }
               
                urun.Ad = model.Ad;
                urun.Fiyat = model.Fiyat;

                _urunRepository.Ekle(urun);

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View(model);
        }

        public IActionResult Guncelle(int id)
        {
            var gelenUrun = _urunRepository.GetirIdile(id);

            UrunGuncelleModel model = new UrunGuncelleModel
            {
                Ad = gelenUrun.Ad,
                Fiyat = gelenUrun.Fiyat,
               
                Id = gelenUrun.Id
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Guncelle(UrunGuncelleModel model)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekUrun = _urunRepository.GetirIdile(model.Id);
            if (model.Resim != null)
            {
                var uzanti = Path.GetExtension(model.Resim.FileName);
                var yeniResimAd = Guid.NewGuid() + uzanti;
                var yuklenecekYer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + yeniResimAd);

                var stream = new FileStream(yuklenecekYer, FileMode.Create);
                model.Resim.CopyTo(stream);

                    guncellenecekUrun.Resim = yeniResimAd;
            }

                guncellenecekUrun.Ad = model.Ad;
                guncellenecekUrun.Fiyat = model.Fiyat;
            _urunRepository.Guncelle(guncellenecekUrun);

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
            return View(model); 
        }
        public IActionResult Sil(int id)
        {
            _urunRepository.Sil(new Urun { Id = id });
            return RedirectToAction("Index");
        }

        public IActionResult AtaKategori(int id)
        {
            var uruneAitKategoriler=_urunRepository.GetirKategoriler(id).Select
                (I=>I.Ad);
            var kategoriler = _kategoriRepository.GetirHepsi();


            TempData["UrunId"] = id;
            List<KategoriAtaModel> list = new List<KategoriAtaModel>();

            foreach (var item in kategoriler)
            {
                KategoriAtaModel model = new KategoriAtaModel();
                model.KategoriId = item.Id;
                model.KategoriAd = item.Ad;
                model.Varmi = uruneAitKategoriler.Contains(item.Ad);

                list.Add(model);
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult AtaKategori(List<KategoriAtaModel> list)
        {
            int urunId = (int)TempData["UrunId"];
            foreach (var item in list)
            {
                if (item.Varmi)
                {
                    _urunRepository.EkleKategori(new UrunKategori
                    {
                        KategoriId = item.KategoriId,
                        UrunId = urunId

                    });
                }
                else
                {
                    _urunRepository.SilKategori(new UrunKategori
                    {
                        KategoriId=item.KategoriId,
                        UrunId=urunId
                    });
                }
            }

            return RedirectToAction("Index");
        }
        
      
    }
}
    

