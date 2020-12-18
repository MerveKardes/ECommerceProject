using E_Ticaret.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.ViewComponents
{
    public class KategoriList :ViewComponent
    {
       private readonly IKategoriRepository _kategoriRepository;
        public KategoriList(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }
       
        public IViewComponentResult Invoke()
        {
            return View(_kategoriRepository.GetirHepsi());
        }
    }
}
