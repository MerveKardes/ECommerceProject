using E_Ticaret.Contexts;
using E_Ticaret.Entities;
using E_Ticaret.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Ticaret.Repositories
{
    public class UrunKategoriRepository : GenericRepository<UrunKategori>, IUrunKategoriRepository
    {
        public UrunKategori GetirFiltreile(Expression<Func<UrunKategori, bool>> filter)
        {
            using var context = new Context();
            return context.UrunKategoriler.FirstOrDefault(filter);
        }
    }
}
