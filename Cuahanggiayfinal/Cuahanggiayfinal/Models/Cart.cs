using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cuahanggiayfinal.Models;
namespace Cuahanggiayfinal.Models
{
    public class Cart
    {
        dbQLBangiayDataContext data = new dbQLBangiayDataContext();
        public int iproductId { set; get; }
        public string sProductName { set; get; }
        public string simg { set; get; }
        public Double dDongia { set; get; }
        public int isoluong { set; get; }
        public int isize { set; get; }
        public Double dthanhtien
        {
            get { return isoluong * dDongia; }
        }
        public Cart(int productId)
        {
            iproductId = productId;
            PRODUCT product = data.PRODUCTs.Single(n => n.productId == iproductId);
            sProductName = product.productName;
            simg = product.img;
            isize = product.size.sizegiay;
            dDongia = double.Parse(product.price.ToString());
            isoluong = 1;
        }
    }
}