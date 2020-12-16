using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Entities
{
    public class Urun
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Resim { get; set; }
        public decimal Fiyat { get; set; }
        public List<UrunKategori> UrunKategoriler { get; set; }

    }
}
