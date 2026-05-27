using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ds3.Models
{
    public class Boss
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 

        [Required]
        public string Location { get; set; } 

        [Required]
        public int Health { get; set; } 

        [Required]
        public int Souls { get; set; } 

        [Required]
        public bool IsRequired { get; set; } 

        [Required]
        public string Weaknesses { get; set; } 

        [Required]
        public string VideoUrl { get; set; } 

        [Required]
        public string ImageUrl { get; set; } 
    }
}