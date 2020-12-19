using E_Ticaret.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.TagHelpers
{
    [HtmlTargetElement("getirKategoriAd")]
    public class KategoriAd:TagHelper
    {
        private readonly IUrunRepository _urunRepository;
        public KategoriAd(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }
        public int UrunId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string data = "";
            var gelenKategoriler=_urunRepository.GetirKategoriler(UrunId).Select(I =>I.Ad);
           

            foreach (var item in gelenKategoriler)
            {
                data += item + " ";
            }
            output.Content.SetContent(data);
        }
    }
}
