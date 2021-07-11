using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cuahanggiayfinal.Models;
namespace Cuahanggiayfinal.Controllers
{
    public class ShoeStoreController : Controller
    {
        dbQLBangiayDataContext data = new dbQLBangiayDataContext();
        
        // GET: ShoeStore
        public ActionResult Index()
        {
            var giaymoi = layproductmoi(8);
            return View(giaymoi);
        }
        private List<PRODUCT> layproductmoi(int count)
        {
            return data.PRODUCTs.OrderByDescending(c => c.ngaycapnhat).Take(count).ToList();
        }
        public ActionResult hang()
        {
            var hang = from h in data.brands select h;
            return PartialView(hang);
        }
        public ActionResult theloai()
        {
            var theloai = from tl in data.CATEGORies select tl;
            return PartialView(theloai);
        }

        public ActionResult sizegiay()
        {
            var sizegiay = from sz in data.sizes select sz;
            return PartialView(sizegiay);
        }
        public ActionResult SPtheohang(int id)
        {
            var product = from b in data.PRODUCTs where b.brandId == id select b;
            return View(product);
        }
        public ActionResult SPtheoloai(int id)
        {
            var product = from b in data.PRODUCTs where b.catId == id select b;
            return View(product);
        }
        public ActionResult Details(int id)
        {
            var product = from s in data.PRODUCTs
                          where s.productId == id
                          select s;
            return View(product.Single());
        }
        public ActionResult SPtheosize(int id)
        {
            var product = from a in data.PRODUCTs where a.sizeId == id select a;
            return View(product);
        }
        private List<PRODUCT> laysphot(int count)
        {
            return data.PRODUCTs.OrderByDescending(c => c.soluongton).Take(count).ToList();
        }
        public ActionResult giayhot()
        {
            var giayhot = laysphot(3);
            return PartialView(giayhot);
        }
        private List<PRODUCT> layspgia(int count)
        {
            return data.PRODUCTs.OrderByDescending(c => c.price).Take(count).ToList();
        }
        public ActionResult laysptheogia()
        {
            var laygia = layspgia(3);
            return PartialView(laygia);
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult nikeAbout()
        {
            return View();
        }
        public ActionResult converseAbout()
        {
            return View();
        }
        public ActionResult adidasAbout()
        {
            return View();
        }
    }
}