using E_Ticaret.CustomExtensions;
using E_Ticaret.Entities;
using E_Ticaret.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace E_Ticaret.Repositories
{
    public class SepetRepository:ISepetRepository
    {
        IHttpContextAccessor _httpContextAccessor;
        public SepetRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void SepeteEkle(Urun urun)
        {
            var gelenListe=_httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");
            if (gelenListe == null)
            {
                gelenListe = new List<Urun>();
                gelenListe.Add(urun);
            }
            else
            {
                gelenListe.Add(urun);
            }

        }
        public void SepettenCikar(Urun urun)
        {
            var gelenListe = 
                _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");
            gelenListe.Remove(urun);

            _httpContextAccessor.HttpContext.Session.SetObject("sepet", gelenListe);
        }
    }
}
