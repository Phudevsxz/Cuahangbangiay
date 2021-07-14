using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cuahanggiayfinal.Models;
using PagedList;
using PagedList.Mvc;

namespace Cuahanggiayfinal.Controllers
{
    public class AdminController : Controller
    {
        dbQLBangiayDataContext data = new dbQLBangiayDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = "Phải nhập tài khoản!";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Phải nhập mật khẩu!";
            }
            else
            {
                ad admin = data.ads.SingleOrDefault(d => d.adminTaikhoan == tendn && d.adminPass == matkhau);
                if (admin != null)
                {
                    Session["Taikhoanadmin"] = admin;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult GetUser()
        {
            return View(data.CUSTOMERs.ToList());
        }
        public ActionResult GetProduct(int ? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.PRODUCTs.ToList().OrderBy(a => a.productId).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Getcat()
        {
            return View(data.CATEGORies.ToList());
        }
        public ActionResult GetSize()
        {
            return View(data.sizes.ToList());
        }
        public ActionResult GetBrand()
        {
            return View(data.brands.ToList());
        }
        public ActionResult GetCart()
        {
            return View(data.carts.ToList());
        }
    }
}