using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ds3.Models
{
    public class IndexViewModel
    {
        public List<rehber> Rehberler { get; set; }

        public List<yorum> Yorumlar { get; set; }

        public string YeniYorumIcerik { get; set; }
    }
}