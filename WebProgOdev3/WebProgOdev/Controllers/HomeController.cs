using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgOdev.Models;

namespace WebProgOdev.Controllers
{
    public class HomeController : Controller
    {
        BitkilerEntities db = new BitkilerEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Kesfet()
        {
            return View(db.Familyalar.ToList());
        }
        public ActionResult KesifDetay(string kesifKodu)
        {
            //Session["kesif"] = kesifKodu;
           // if (Session["kesif"] == null) Session["kesif"] = "sukulent";
            List<TurDetay> fml = db.TurDetay.Where(x => x.Familyalar.familyaKod == kesifKodu).ToList();
            
            return View(fml);
        }
        public ActionResult ResimGoster(int resimID)
        {
            var resim = db.TurDetay.FirstOrDefault(x => x.turDetayId == resimID);
            return View(resim);
        }
    }
}