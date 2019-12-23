using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipProg.Models;
using PagedList;
using PagedList.Mvc;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace TakipProg.Controllers
{
    [Authorize(Roles ="Worker")]
    public class HomeController : Controller
    {

        DataContext db = new DataContext();

       


        // GET: Home
        public ActionResult Index()
        {
           

            return View();
        }
        
        [HttpGet]
        public ActionResult CalismaSaatiEkle()
        {

            ViewBag.liste = new SelectList(db.makinalar.ToList(),"Id","MakinaAdi");




            return View();
        }


        [HttpPost]
        public ActionResult CalismaSaatiEkle(CalismaModel calismaModel)
        {
            
            if (ModelState.IsValid)
            {
                Calisma calisma1 = new Calisma();
                calisma1.BaslananMekik = calismaModel.BaslananMekik;
                calisma1.BitisMekik = calismaModel.BitisMekik;
                
                calisma1.Gün = calismaModel.Gün;
                calisma1.Not = calismaModel.Not;
                calisma1.MakinaId = calismaModel.MakinaId;
                
                calisma1.Mekik = calisma1.BitisMekik - calisma1.BaslananMekik;
                

                db.calismalar.Add(calisma1);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            else
            {
                
            }


            return View();
            

           
        }

        public ActionResult Calismalar(int? SayfaNo,string GunString,string MakinaString,string suankiFiltre,string suankiFiltre2)
        {
            if (GunString!=null)
            {
                SayfaNo = 1;
            }
            else
            {
                GunString = suankiFiltre;
            }
            ViewBag.SuankiFiltre = GunString;
            if (MakinaString != null)
            {
                SayfaNo = 1;
            }
            else
            {
                MakinaString = suankiFiltre2;
            }
            ViewBag.SuankiFiltre2 = MakinaString;

            var calismas = db.calismalar
            .Select(i => new Model2()
            {
                Id = i.Id,
                Gün = i.Gün,
                BaslananMekik = i.BaslananMekik,
                BitisMekik = i.BitisMekik,
                Mekik = i.Mekik,
                Not = i.Not,
                Makina = i.calisilanMakina.MakinaAdi




            }).OrderBy(i=>i.Gün);
            ViewBag.Sayi = calismas.Count();

            if (!String.IsNullOrEmpty(GunString))
            {
                calismas = calismas.Where(i => i.Gün.Contains(GunString)).OrderBy(i=>i.Gün);
                ViewBag.Sayi = calismas.Count();
            }
            if (!String.IsNullOrEmpty(MakinaString))
            {
                calismas = calismas.Where(i => i.Makina.Contains(MakinaString)).OrderBy(i => i.Gün);
                ViewBag.Sayi = calismas.Count();
            }
            
            int pageSize = 5;
            int pageNumber = (SayfaNo ?? 1);
            Session["calismas"] = calismas.ToPagedList<Model2>(pageNumber, 50000);
            return View(calismas.ToPagedList(pageNumber, pageSize));

        }



        public ActionResult ExportToExcel(string GunString,string MakinaString)
        {
            DataTable dtCalismalar = new DataTable();
            dtCalismalar.Columns.AddRange(new DataColumn[6]
            {
                new DataColumn("Id"),
                new DataColumn("Gün"),
                new DataColumn("Baslanan Mekik"),
                new DataColumn("Bitis Mekik"),
                new DataColumn("Mekik"),
                new DataColumn("Makina")
            });

            var calismas = (PagedList<Model2>)Session["calismas"];
            

            foreach (var item in calismas)
            {
                dtCalismalar.Rows.Add(item.Id.ToString(), item.Gün, item.BaslananMekik.ToString(), item.BitisMekik.ToString(), item.Mekik.ToString(), item.Makina);
            }



            string fileName = "Calismalar";
            var grid = new GridView();

            grid.DataSource = dtCalismalar;
            grid.DataBind();

            Response.ClearContent();
            Response.Charset = "utf-8";
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xls");

            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();

            return RedirectToAction("Calismalar");

        }
    }
}