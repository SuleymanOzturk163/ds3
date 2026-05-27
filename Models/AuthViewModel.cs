using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ds3.Models
{
    public class AuthViewModel
    {
        public string RegisterUsername { get; set; }
        public string RegisterEmail { get; set; }
        public string RegisterPassword { get; set; }

        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }
    }
}