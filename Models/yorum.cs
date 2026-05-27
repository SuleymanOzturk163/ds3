using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ds3.Models
{
    public class yorum
    {
        public int Id { get; set; }
        public string Icerik { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;

        public string? KullaniciId { get; set; }
        public string KullaniciAd { get; set; } 

        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public bool IsPinned { get; set; } 
    }
}