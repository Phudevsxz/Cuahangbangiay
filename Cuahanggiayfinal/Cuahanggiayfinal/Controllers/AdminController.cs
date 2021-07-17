using System;
using System.Collections.Generic;
using System.IO;
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
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("login", "Admin");
            else
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
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("login", "Admin");
            else
            return View(data.CUSTOMERs.ToList());
        }
        public ActionResult GetProduct(int ? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("login", "Admin");
            else
                return View(data.PRODUCTs.ToList().OrderBy(a => a.productId).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Getcat()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("login", "Admin");
            else
                return View(data.CATEGORies.ToList());
        }
        public ActionResult GetSize()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("login", "Admin");
            else
                return View(data.sizes.ToList());
        }
        public ActionResult GetBrand()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("login", "Admin");
            else
                return View(data.brands.ToList());
        }
        public ActionResult GetCart()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("login", "Admin");
            else
                return View(data.carts.ToList());
        }

        public ActionResult ThemBrand()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemBrand(FormCollection collection,brand brand)
        {
            var br = collection["BranName"];
            if( String.IsNullOrEmpty(br))
            {
                ViewData["Loi"] = "Không được để trống!";
            }
            else
            {
                brand.branName = br;
                data.brands.InsertOnSubmit(brand);
                data.SubmitChanges();
                return RedirectToAction("GetBrand");
            }
            return this.ThemBrand();
        }
        //xoa Brand
        [HttpGet]
        public ActionResult XoaBrand(int id)
        {
            brand brand = data.brands.SingleOrDefault(n => n.brandId == id);
            ViewBag.brandId = brand.brandId;
            if(brand == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(brand);
        }
        [HttpPost,ActionName("XoaBrand")]
        public ActionResult XacnhanxoaBrand(int id)
        {
            brand brand = data.brands.SingleOrDefault(n => n.brandId == id);
            ViewBag.brandId = brand.brandId;
            if (brand == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.brands.DeleteOnSubmit(brand);
            data.SubmitChanges();
            return RedirectToAction("GetBrand");
        }
        public ActionResult ThemCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemCategory(FormCollection collection,CATEGORY cat)
        {
            var br = collection["catName"];
            if (String.IsNullOrEmpty(br))
            {
                ViewData["Loi"] = "Không được để trống!";
            }
            else
            {
                cat.catName = br;
                data.CATEGORies.InsertOnSubmit(cat);
                data.SubmitChanges();
                return RedirectToAction("Getcat");
            }
            return this.ThemCategory();
        }
        [HttpGet]
        public ActionResult XoaCategory(int id)
        {
            CATEGORY cat = data.CATEGORies.SingleOrDefault(n => n.catId == id);
            ViewBag.CatId = cat.catId;
            if (cat == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cat);
        }
        [HttpPost, ActionName("XoaCategory")]
        public ActionResult XacnhanxoaCategory(int id)
        {
            CATEGORY cat = data.CATEGORies.SingleOrDefault(n => n.catId == id);
            ViewBag.CatId = cat.catId;
            if(cat == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.CATEGORies.DeleteOnSubmit(cat);
            data.SubmitChanges();
            return RedirectToAction("Getcat");
        }
        public ActionResult ThemSize()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemSize(FormCollection collection, size size)
        {
            var br = collection["sizegiay"];
            if (String.IsNullOrEmpty(br))
            {
                ViewData["Loi"] = "Không được để trống!";
            }
            else
            {
                data.sizes.InsertOnSubmit(size);
                data.SubmitChanges();
                return RedirectToAction("GetSize");
            }
            return this.ThemSize();
        }
        [HttpGet]
        public ActionResult XoaSize(int id)
        {
            size size = data.sizes.SingleOrDefault(n => n.sizeId == id);
            ViewBag.sizeId = size.sizeId;
            if (size == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(size);
        }
        [HttpPost,ActionName("XoaSize")]
        public ActionResult XacnhanxoaSize(int id)
        {
            size size = data.sizes.SingleOrDefault(n => n.sizeId == id);
            ViewBag.sizeId = size.sizeId;
            if( size == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.sizes.DeleteOnSubmit(size);
            data.SubmitChanges();
            return RedirectToAction("GetSize");
        }
        [HttpGet]
        public ActionResult ThemProduct()
        {
            ViewBag.SizeId = new SelectList(data.sizes.ToList().OrderByDescending(n => n.sizegiay), "sizeId", "sizegiay");
            ViewBag.catId = new SelectList(data.CATEGORies.ToList().OrderByDescending(n => n.catId), "catId", "catName");
            ViewBag.brandId = new SelectList(data.brands.ToList().OrderByDescending(n => n.brandId), "brandId", "branName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemProduct(PRODUCT giay, HttpPostedFileBase fileUpload)
        {
            ViewBag.SizeId = new SelectList(data.sizes.ToList().OrderByDescending(n => n.sizegiay), "sizeId", "sizegiay");
            ViewBag.catId = new SelectList(data.CATEGORies.ToList().OrderByDescending(n => n.catId), "catId", "catName");
            ViewBag.brandId = new SelectList(data.brands.ToList().OrderByDescending(n => n.brandId), "brandId", "branName");

            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh của giày";
                return View();
            }
            else
            {
                if(ModelState.IsValid)
                {
                    var FileName = Path.GetFileName(fileUpload.FileName);

                    var path = Path.Combine(Server.MapPath("~/images"), FileName);
                    if(System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    giay.img = FileName;
                    data.PRODUCTs.InsertOnSubmit(giay);
                    data.SubmitChanges();
                }
                return RedirectToAction("GetProduct");
            }    
        }
        public ActionResult ChitietProduct(int id)
        {
            PRODUCT giay = data.PRODUCTs.SingleOrDefault(n => n.productId == id);
            ViewBag.ProductId = giay.productId;
            if(giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giay);
        }
        public ActionResult XoaProduct(int id)
        {
            PRODUCT giay = data.PRODUCTs.SingleOrDefault(n => n.productId == id);
            ViewBag.ProductId = giay.productId;
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giay);
        }
        [HttpPost,ActionName("XoaProduct")]
        public ActionResult xacnhanXoaProduct(int id)
        {
            PRODUCT giay = data.PRODUCTs.SingleOrDefault(n => n.productId == id);
            ViewBag.ProductId = giay.productId;
            if(giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.PRODUCTs.DeleteOnSubmit(giay);
            data.SubmitChanges();
            return RedirectToAction("GetProduct");
        }
        //chỉnh sửa sản phẩm:
        [HttpGet]
        public ActionResult SuaProduct(int id)
        {
            ViewBag.SizeId = new SelectList(data.sizes.ToList().OrderByDescending(n => n.sizegiay), "sizeId", "sizegiay");
            ViewBag.catId = new SelectList(data.CATEGORies.ToList().OrderByDescending(n => n.catId), "catId", "catName");
            ViewBag.brandId = new SelectList(data.brands.ToList().OrderByDescending(n => n.brandId), "brandId", "branName");
            PRODUCT giay = data.PRODUCTs.SingleOrDefault(n => n.productId == id);
            ViewBag.ProductId = giay.productId;
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View("GetProduct");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaProduct(PRODUCT giay, HttpPostedFileBase fileUpload)
        {
            ViewBag.SizeId = new SelectList(data.sizes.ToList().OrderByDescending(n => n.sizegiay), "sizeId", "sizegiay");
            ViewBag.catId = new SelectList(data.CATEGORies.ToList().OrderByDescending(n => n.catId), "catId", "catName");
            ViewBag.brandId = new SelectList(data.brands.ToList().OrderByDescending(n => n.brandId), "brandId", "branName");
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh giày";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var FileName = Path.GetFileName(fileUpload.FileName);

                    var path = Path.Combine(Server.MapPath("~/images"), FileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    giay.img = FileName;

                    UpdateModel(giay);
                    data.SubmitChanges();
                }
                return RedirectToAction("GetProduct");
            }
        }
    }
}