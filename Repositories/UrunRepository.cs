using E_Ticaret.Contexts;
using E_Ticaret.Entities;
using E_Ticaret.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Repositories
{
    public class UrunRepository:GenericRepository<Urun>,IUrunRepository
    {
        public List<Kategori> GetirKategoriler(int urunId)
        {
            using var context = new Context();
          return  context.Urunler.Join(context.UrunKategoriler, urun => urun.Id, urunKategori => urunKategori.UrunId, (u, uc) => new
            {
                urun = u,
                urunKategori = uc
            }).Join(context.Kategoriler, iki => iki.urunKategori.KategoriId, kategori => kategori.Id, (uc, k) => new
            {
                urun = uc.urun,
                kategori = k,
                urunKategori = uc.urunKategori
            }).Where(I => I.urun.Id == urunId).Select(I => new Kategori
            {
                Ad = I.kategori.Ad,
                Id = I.kategori.Id


            }).ToList();
        }
    }
}
