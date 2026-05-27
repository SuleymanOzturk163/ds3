using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ds3.Models
{
    public class RehberViewModel
    {
        [Required(ErrorMessage = "REHBER BAŞLIĞI ZORUNLUDUR!")]
        [StringLength(100, ErrorMessage = "BAŞLIK ÇOK UZUN!")]
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "ÖZET ALANI ZORUNLUDUR!")]
        [Display(Name = "Özet")]
        public string Ozet { get; set; }

        [Required(ErrorMessage = "İÇERİK ALANI ZORUNLUDUR!")]
        [Display(Name = "İçerik")]
        public string Icerik { get; set; }


        [Required(ErrorMessage = "Detaylı içerik boş olamaz!")]
        public string UzunIcerik { get; set; }

        [Required(ErrorMessage = "REHBER İÇİN BİR RESİM SEÇMELİSİNİZ!")]
        [Display(Name = "Rehber Resmi")]
        public IFormFile ResimDosyasi { get; set; }

        [Required(ErrorMessage = "Video linki eklemelisin.")]
        [Url(ErrorMessage = "Geçerli bir URL adresi girin.")]
        public string VideoUrl { get; set; }

        [Required(ErrorMessage = "YAZAR ADI ZORUNLUDUR!")]
        [Display(Name = "Yazar")]
        public string Yazar { get; set; } = "Suleyman";


    }
}