using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Entities
{
    public class Kategori
    {
        public int Id { get; set; }
        public string Ad{ get; set; }

        public List<UrunKategori> UrunKategoriler { get; set; }
    }
}
