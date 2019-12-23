using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TakipProg.Models
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            List<Makina> makinalar = new List<Makina>()
            {
                 new Makina(){MakinaAdi="Makina 1"},
                 new Makina(){MakinaAdi="Makina 2"},
                 new Makina(){MakinaAdi="Makina 3"},
                 new Makina(){MakinaAdi="Makina 4"},
                 new Makina(){MakinaAdi="Makina 5"}
            };
            foreach (var item in makinalar)
            {
                context.makinalar.Add(item);
            }
            context.SaveChanges();

           

            base.Seed(context);
        }
    }
}