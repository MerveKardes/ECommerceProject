using E_Ticaret.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Repositories
{
    public class GenericRepository<Tablo> where Tablo:class,new()
    {
        public void Ekle(Tablo tablo)
        {
            using var context = new Context();
            context.Set<Tablo>().Add(tablo);
            context.SaveChanges();
        }

        public void Guncelle(Tablo tablo)
        {
            using var context = new Context();
            context.Set<Tablo>().Update(tablo);
            context.SaveChanges();
        }

        public void Sil(Tablo tablo)
        {
            using var context = new Context();
            context.Set<Tablo>().Remove(tablo);
            context.SaveChanges();
        }

        public List<Tablo> GetirHepsi()
        {
            using var context = new Context();
            return context.Set<Tablo>().OrderByDescending(I=>I.Id).ToList();
        }
        public Tablo GetirIdile(int id)
        {
            using var context = new Context();
            return context.Set<Tablo>().Find(id);
        }
    }
}
