using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace TakipProg.Models
{
    public class Calisma
    {
        public int Id { get; set; }

        public int MakinaId { get; set; }

        

        [Required]
        public string Gün { get; set; }

        [Required]
        public int BaslananMekik { get; set; }

        [Required]
        public int BitisMekik { get; set; }

        public int Mekik { get; set; }
        public string Not { get; set; }

        
        public Makina calisilanMakina { get; set; }

        public ApplicationUser Calisan { get; set; }

    }
}