using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ds3.Models
{
    public class LocationViewModel
    {

        [Required(ErrorMessage = "ÜRÜN ADI ZORUNLU")]
        [StringLength(50, ErrorMessage = "ÇOKUZUN")]
        [Display(Name = "başlık")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ALT BAŞLIK ZORUNLU")]
        [Display(Name = "alt başlık")]
        public string InfoSubtitle { get; set; }

        
        [Required(ErrorMessage = "RESİM ZORUNLU")]
        [Display(Name = "Resim")]
        public IFormFile ImageUrl { get; set; }

        [Required(ErrorMessage = "AÇIKLAMA ZORUNLU")]
        [Display(Name = "açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "KATEGORİ ZORUNLU")]
        [Display(Name = "kategori")]
        public bool Category { get; set; }
    }
}