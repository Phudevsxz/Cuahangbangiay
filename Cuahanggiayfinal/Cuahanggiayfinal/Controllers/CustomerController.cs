using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cuahanggiayfinal.Models;
namespace tiembangiayfinal.Controllers
{
    public class CustomerController : Controller
    {
        dbQLBangiayDataContext dt = new dbQLBangiayDataContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        public ActionResult DangKy(FormCollection collection, CUSTOMER kh)
        {
            var hoten = collection["Hoten"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["DienThoai"];
            var city = collection["City"];
            var country = collection["country"];
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Ho ten khong dc phep de trong";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "ten dang nhap ko dc de trong";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phai nhap mat khau";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phai nhap lai mat khau";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email ko dc de trong";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phai nhap dien thoai";
            }
            if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi7"] = "dia chi dau ban oi";
            }
            if (String.IsNullOrEmpty(city))
            {
                ViewData["Loi8"] = "phai nhap thanh pho";
            }
            if (String.IsNullOrEmpty(country))
            {
                ViewData["Loi9"] = "Phai nhap Country";
            }
            else
            {
                kh.customerName = hoten;
                kh.taikhoan = tendn;
                kh.matkhau = matkhau;
                kh.customeremail = email;
                kh.address = diachi;
                kh.city = city;
                kh.country = country;
                kh.phone = dienthoai;
                dt.CUSTOMERs.InsertOnSubmit(kh);
                dt.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                CUSTOMER kh = dt.CUSTOMERs.SingleOrDefault(n => n.taikhoan == tendn && n.matkhau == matkhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Đăng Nhập thành công ";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "ShoeStore");
                }
                else
                    ViewBag.ThongBao = "Ten Dang nhap Hoac mat khau ko dung";
            }
            return View();
        }
    }
}