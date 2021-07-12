using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cuahanggiayfinal.Models;
namespace Cuahanggiayfinal.Controllers
{
    public class CartController : Controller
    {
        dbQLBangiayDataContext data = new dbQLBangiayDataContext();
        // GET: Cart
        public List<Cart> laycart()
        {
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart == null)
            {
                lstCart = new List<Cart>();
                Session["Cart"] = lstCart;
            }
            return lstCart;
        }
        public ActionResult themCart(int iproductId, string strURL)
        {
            List<Cart> lstCart = laycart();
            Cart sanpham = lstCart.Find(n => n.iproductId == iproductId);
            if (sanpham == null)
            {
                sanpham = new Cart(iproductId);
                lstCart.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.isoluong++;
                return Redirect(strURL);
            }

        }
        private int Tongsoluong()
        {
            int iTongsoluong = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                iTongsoluong = lstCart.Sum(n => n.isoluong);
            }
            return iTongsoluong;
        }
        private double Tongtien()
        {
            double iTongtien = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                iTongtien = lstCart.Sum(n => n.dthanhtien);
            }
            return iTongtien;
        }
        public ActionResult Cart()
        {
            List<Cart> lstCart = laycart();
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "ShoeStore");
            }
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstCart);
        }
        public ActionResult CartPartial()
        {
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return PartialView();
        }
        //Ham xoa gio hangf
        public ActionResult XoaCart(int iMasp)
        {
            List<Cart> lstcart = laycart();
            Cart sanpham = lstcart.SingleOrDefault(n => n.iproductId == iMasp);

            if (sanpham != null)
            {
                lstcart.RemoveAll(n => n.iproductId == iMasp);
                return RedirectToAction("Cart");
            }
            if (lstcart.Count == 0)
            {
                return RedirectToAction("Index", "ShoeStore");
            }
            return RedirectToAction("Cart");
        }
        //cap nhat
        [HttpPost]
        public ActionResult CapnhatCart(int ? iMasp, FormCollection f)
        {
            List<Cart> lstcart = laycart();
            string count = f["txtSoluong"];
            Cart sanpham = lstcart.SingleOrDefault(n => n.iproductId == iMasp);
            if (lstcart != null && count!= null)
            {
                sanpham.isoluong = int.Parse(count);
            }
            return RedirectToAction("Cart");
        }
        //xoa tất cả
        public ActionResult XoatatcaCart()
        {
            List<Cart> lstcart = laycart();
            lstcart.Clear();
            return RedirectToAction("Index", "ShoeStore");
        }
        // dat hang
        [HttpGet]
        public ActionResult Dathang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "Customer");
            }
            if (Session["Taikhoan"] == null)
            {
                return RedirectToAction("Index", "ShoeStore");
            }
            List<Cart> lstcart = laycart();
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstcart);
        }
        [HttpPost]
        public ActionResult Dathang(FormCollection collection)
        {
            ViewBag.Tongtien = Tongtien();
            //them hang
            cart cr = new cart();
            CUSTOMER kh = (CUSTOMER)Session["Taikhoan"];
            List<Cart> gh = laycart();
            cr.customerId = kh.customerId;
            cr.tongtien = (decimal)ViewBag.Tongtien;
            cr.ngaydat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            cr.ngaygiao = DateTime.Parse(ngaygiao);
            cr.tinhtrang = false;
            data.carts.InsertOnSubmit(cr);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                chitietdonhang ctdh = new chitietdonhang();
                ctdh.cartId = cr.cartId;
                ctdh.productId = item.iproductId;
                ctdh.soluong = item.isoluong;
                ctdh.Dongia = (decimal)item.dDongia;

                data.chitietdonhangs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["Cart"] = null;
            return RedirectToAction("Xacnhandonhang", "Cart");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}