using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace TakipProg.Models
{
    public class Model2
    {
      public int Id { get; set; }

        public string Gün { get; set; }

        
        public int BaslananMekik { get; set; }

      
        public int BitisMekik { get; set; }

        public int Mekik { get; set; }
        public string Not { get; set; }
        public string Makina { get; set; }

       
    }
}