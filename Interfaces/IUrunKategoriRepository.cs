using E_Ticaret.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Ticaret.Interfaces
{
    public interface IUrunKategoriRepository:IGenericRepository<UrunKategori>
    {
        UrunKategori GetirFiltreile(Expression<Func<UrunKategori, bool>> filter);
    }

   
}
