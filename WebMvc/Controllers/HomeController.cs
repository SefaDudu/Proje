
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Business.EFatura;
using Business.tr.com.edmbilisim.test;

namespace WebMvc.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
          





            return View();
        }

        [HttpPost]
        public ActionResult Index(EFatura EFatura)
        {
            if (Request.Form["btnFaturaOlustur"] == "btnFaturaOlustur")//btnFaturaOlustur tıklandı
            {
                if (ModelState.IsValid)
                {
                    EFaturaBll.EFaturaAyarlar eFaturaAyarlar = new EFaturaBll.EFaturaAyarlar();
                    eFaturaAyarlar.XsltDosyaYolu = Server.MapPath("~/App_Data/") + "general.xslt";
                    eFaturaAyarlar.XmlDosyaKlasor = Server.MapPath("~/App_Data/");//Klasöre IUSR Yazma İzni vermelisiniz

                    eFaturaAyarlar.EFaturaGonderenCariKod = "120 0000 000 001";
                    eFaturaAyarlar.EFaturaImzalayanTcKimlikNo = "11111111111";

                    eFaturaAyarlar.EFaturaKesilenMusteriTcKimlikNo = "11111111111";
                    eFaturaAyarlar.EFaturaKesilenMusteriAd = "HASAN";
                    eFaturaAyarlar.EFaturaKesilenMusteriSoyad = "YILMAZ";
                    eFaturaAyarlar.EFaturaKesilenMusteriEmail = "hasanyilmaz@gmail.com";
                    eFaturaAyarlar.EFaturaKesilenMusteriTelefon = "312-4444444";
                    eFaturaAyarlar.EFaturaImzalayanAdresIlAd = "ANKARA";
                    eFaturaAyarlar.EFaturaKesenKurumMersisNo = "1111222233334444";
                    eFaturaAyarlar.EFaturaKesenKurumVergiDairesi = "ÇANKAYA VD";

                    EFaturaBll.EFaturaOlustur(EFatura, eFaturaAyarlar);//Hata vermezse App_Data/ klasörü altına xml dosyası kaydedilecek

                    ViewBag.FaturaNo = EFatura.FaturaNo;

                }
            }
            else if (Request.Form["btnYazdir"] == "btnYazdir") //btnYazdir tıklandı
            {
                TempData["XmlString"] = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/" + EFatura.FaturaNo + ".xml"));

                return RedirectToAction("Yazdir");

            }

            return View();
        }
        public ActionResult Yazdir()
        {
            ViewBag.XmlString = TempData["XmlString"];
            return View();
        }
    }
}