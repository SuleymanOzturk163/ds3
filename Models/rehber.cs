using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ds3.Models
{
    public class rehber
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Ozet { get; set; }
        public string Icerik { get; set; } 
        public string ResimUrl { get; set; } 
        public string Yazar { get; set; } = "Suleyman";
        public DateTime Tarih { get; set; } = DateTime.Now;

        public string? UzunIcerik { get; set; }
        public string? VideoUrl { get; set; }
    }
}