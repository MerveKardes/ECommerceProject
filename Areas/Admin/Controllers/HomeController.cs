using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Ticaret.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
         private readonly IUrunRepository _urunRepository;

        public HomeController(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }


        public IActionResult Index()
        {
            return View(_urunRepository.GetirHepsi());
        }
    }
}
