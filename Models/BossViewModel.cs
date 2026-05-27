using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ds3.Models
{
    public class BossViewModel
    {
        [Required(ErrorMessage = "Boss adını yazmak zorunludur ashen one!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu canavarın mekanını belirtmek zorunludur!")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Can puanını girmelisin!")]
        public int Health { get; set; }

        [Required(ErrorMessage = "Verdiği ruh miktarını belirtmelisin!")]
        public int Souls { get; set; }

        [Required]
        public bool IsRequired { get; set; } 

        [Required(ErrorMessage = "En az bir zayıflık yazmalısın (Virgülle ayırabilirsin)!")]
        public string Weaknesses { get; set; }

        [Required(ErrorMessage = "Rehber video linkini boş geçemezsin!")]
        public string VideoUrl { get; set; }

        [Required(ErrorMessage = "Boss'un haşmetli bir görselini seçmelisin!")]
        public IFormFile ImageUrl { get; set; }
    }
}