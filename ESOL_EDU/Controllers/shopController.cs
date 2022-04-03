using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESOL_BO.DbAccess;
using ESOL_BO.Ecommerce;
using ESOL_EDU.Helper;

namespace ESOL_EDU.Controllers
{
    public class shopController : Controller
    {
        public ActionResult products(string slug, int catId)
        {
            var list = DataAccess.EcommerceProduct.GetAllProduct(slug, catId);
            ViewBag.Category = DataAccess.EcommerceCategory.GetByOrg("select * from EcommerceCategory where IsActive=1 order by CategoryName asc"); 
            return View(list);
        }
        public ActionResult product(string slug)
        {
            string where = "R";
            var single = DataAccess.EcommerceProduct.GetSingleProduct(slug);
            ViewBag.RelatedProduct = DataAccess.EcommerceProduct.GetProductRelated(single.SubCategoryId, where);
            // also view recent customer
            List<EcommerceProductDomain> lists = new List<EcommerceProductDomain>();
            if (Session["RecentView"] != null)
            {
                lists = Session["RecentView"] as List<EcommerceProductDomain>;
            }
            List<EcommerceProductDomain> listItem = DataAccess.EcommerceProduct.GetByProductId(single.ProductId);
            foreach (EcommerceProductDomain item in listItem)
            {
                EcommerceProductDomain domain = new EcommerceProductDomain();
                var badge = DataAccess.EcommerceBadge.GetById("select * from EcommerceBadge where BadgeId='" + item.BadgeId+"'");
                domain.ProductId = item.ProductId;
                domain.ProductName = item.ProductName;
                domain.Slug = item.Slug;
                domain.ImagePath = item.ImagePath;
                domain.Discount = item.Discount;
                domain.RegularPrice = item.RegularPrice;
                domain.SalePrice = item.SalePrice;
                domain.BadgeName = badge.BadgeName;                      
                lists.Add(domain);
            }
            Session["RecentView"] = lists;
            
            // Recently Views customer wise
            if(Session["CustomerPortal"] != null)
            {
                RecentlyViews views = new RecentlyViews();
                views.ViewId = 0;
                views.CustomerId = CustomerHelper.CustomerId;
                views.ProductId = single.ProductId;
                views.ViewDate = DateTime.Now;
                views.StatementType = "Insert";
                DataAccess.EcommerceOrder.InsertRecentlyViews(views);
            }       
            return View(single);
        }
        public ActionResult cart()
        {
            ViewBag.Message = TempData["SaveMessage"];
            return View();
        }
        public ActionResult addToCart(int id)
        {
            int quantity = 1;
            List<EcommerceOrderDetailsDomain> lists = new List<EcommerceOrderDetailsDomain>();
            if (Session["OrderDetails"] != null)
            {
                lists = Session["OrderDetails"] as List<EcommerceOrderDetailsDomain>;
            }
            EcommerceOrderDetailsDomain newList = lists.Where(e => e.ProductId == id).FirstOrDefault();
            if (newList != null)
            {
                lists.Remove(newList);
            }
            List<EcommerceProductDomain> listItem = DataAccess.EcommerceProduct.GetByProductId(id);
            foreach (EcommerceProductDomain item in listItem)
            {
                EcommerceOrderDetailsDomain domain = new EcommerceOrderDetailsDomain();
                domain.ProductId = item.ProductId;
                domain.ProductName = item.ProductName;
                domain.Quantity = quantity;
                domain.ItemPrice = Convert.ToDouble(item.SalePrice);
                domain.TotalPrice = Convert.ToDouble(quantity) * Convert.ToDouble(item.SalePrice);
                domain.Slug = item.Slug;
                domain.ImageName = item.ImagePath;
                domain.SKU = item.SKU;
                lists.Add(domain);
            }
            Session["OrderDetails"] = lists;
            if (Session["CustomerPortal"] != null)
            {
                CommonHelper.Delete("EcommerceWishlist", "where ProductId='" + id + "' and CustomerId='"+CustomerHelper.CustomerId+"'");
            }
            TempData["SaveMessage"] = "Add to cart successfully";
            return RedirectToAction("cart");
        }
        public ActionResult remove(int id)
        {
            List<EcommerceOrderDetailsDomain> lists = new List<EcommerceOrderDetailsDomain>();
            if (Session["OrderDetails"] != null)
            {
                lists = Session["OrderDetails"] as List<EcommerceOrderDetailsDomain>;
            }
            EcommerceOrderDetailsDomain newList = lists.Where(e => e.ProductId == id).FirstOrDefault();
            if (newList != null)
            {
                lists.Remove(newList);
            }
            Session["OrderDetails"] = lists;
            TempData["SaveMessage"] = "Remove to cart successfully";
            return RedirectToAction("cart");
        }
        public ActionResult checkout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProcedToCheckout(FormCollection formcoll)
        {
            List<EcommerceOrderDetailsDomain> lists = new List<EcommerceOrderDetailsDomain>();                 

            for (int i = 0; i < formcoll.Count / 2; i++)
            {
                int pID = Convert.ToInt32(formcoll["shcartID-" + i + ""]);
                int qty = Convert.ToInt32(formcoll["Qty-" + i + ""]);
                EcommerceProductDomain item = DataAccess.EcommerceProduct.GetById(pID);
                EcommerceOrderDetailsDomain domain = new EcommerceOrderDetailsDomain();
                domain.ProductId = item.ProductId;
                domain.ProductName = item.ProductName;
                domain.Quantity = qty;
                domain.ItemPrice = Convert.ToDouble(item.SalePrice);
                domain.TotalPrice = Convert.ToDouble(qty) * Convert.ToDouble(item.SalePrice);
                domain.Slug = item.Slug;
                domain.ImageName = item.ImagePath;
                domain.SKU = item.SKU;
                if (domain.ProductId == 0)
                {

                }
                else
                {
                    lists.Add(domain);
                }                
            }
            Session["OrderDetails"] = lists;
            return RedirectToAction("CheckOut");
        }
        public ActionResult wishlist()
        {
            if (Session["CustomerPortal"] != null)
            {
                ViewBag.Wishlist = DataAccess.EcommerceWishlist.GetByCustomerId(CustomerHelper.CustomerId);
                ViewBag.Message = TempData["SaveMessage"];
            }
            return View();
        }
        public ActionResult wishlistAdd(int id)
        {
            EcommerceWishlistDomain wishlist = new EcommerceWishlistDomain();
            wishlist.WishlistId = 0;
            wishlist.CustomerId = CustomerHelper.CustomerId;
            wishlist.ProductId = id;
            wishlist.CreatedDate = DateTime.Now;
            wishlist.isActive = true;
            wishlist.StatementType = "Insert";
            DataAccess.EcommerceWishlist.InsertOrUpdate(wishlist);
            return RedirectToAction("wishlist");
        }
        public ActionResult wishlistRemove(int id)
        {
            CommonHelper.Delete("EcommerceWishlist", "where WishlistId='" + id + "'");
            TempData["SaveMessage"] = "Remove to wishlist successfully";
            return RedirectToAction("wishlist");
        }
        public ActionResult placeOrder(EcommerceOrderDomain domain,FormCollection getCheckoutDetails)
        {
            EcommerceShippingDetailsDomain shpDetails = new EcommerceShippingDetailsDomain();
            shpDetails.Name = getCheckoutDetails["Name"];
            shpDetails.Email = getCheckoutDetails["Email"];
            shpDetails.Mobile = getCheckoutDetails["Mobile"];
            shpDetails.Address = getCheckoutDetails["Address"];
            shpDetails.City = getCheckoutDetails["City"];
            shpDetails.PostCode = getCheckoutDetails["PostCode"];
            shpDetails.IsActive = true;
            shpDetails.CreatedBy = getCheckoutDetails["CustomerId"];
            shpDetails.UpdatedBy = getCheckoutDetails["CustomerId"];
            shpDetails.StatementType = "Insert";
            int shpId = DataAccess.EcommerceShippingDetails.InsertOrUpdate(shpDetails);
            if (shpId > 0)
            {
                EcommerceOrderDomain order = new EcommerceOrderDomain();                
                order.Year = DateTime.Now.ToString("yy");
                order.Day = DateTime.Now.ToString("MM");
                order.InvoiceNo = order.Year + order.Day + DataAccess.EcommerceOrder.AutoId();
                order.CustomerId = Convert.ToInt32(getCheckoutDetails["CustomerId"]);
                order.ShippingId = shpId;
                order.PaymentId = domain.PaymentId;
                order.Discount = Convert.ToDouble(getCheckoutDetails["Discount"]);
                order.Taxes = 0;
                order.CouponId = 0;//Convert.ToInt32(getCheckoutDetails["CouponId"]);
                order.TotalAmount = Convert.ToDouble(getCheckoutDetails["TotalAmount"]);
                order.IsCompleted = false;
                order.OrderDate = DateTime.Now;
                order.IPAddress = Request.UserHostAddress;
                order.Browser = Request.Browser.Browser;
                order.StatementType = "Insert";
                int orId = DataAccess.EcommerceOrder.InsertOrUpdate(order);
                // EcommerceOrderRequestUpdate
                EcommerceOrderRequestUpdate update = new EcommerceOrderRequestUpdate();
                update.UpdateId = 0;
                update.OrderId = orId;
                update.UpdateDateTime = DateTime.Now;
                update.UpdateById = 1;
                update.StatusId = 3;
                update.UserTypeId = 1; // software admin
                update.Remarks = "New Order";
                update.StatementType = "Insert";
                DataAccess.EcommerceOrder.InsertOrderRequestUpdate(update);
                if (orId > 0)
                {
                    foreach (EcommerceOrderDetailsDomain details in (List<EcommerceOrderDetailsDomain>)Session["OrderDetails"])
                    {
                        details.OrderId = orId;
                        details.ProductId = details.ProductId;
                        details.ItemPrice = details.ItemPrice;
                        details.Quantity = details.Quantity;
                        details.Discount = 0;
                        details.TotalPrice = details.TotalPrice;
                        details.OrderDate = DateTime.Now;
                        details.StatementType = "Insert";
                        DataAccess.EcommerceOrderDetails.InsertOrUpdate(details);
                    }
                }
            }        
            return RedirectToAction("Index", "ThankYou");
        }
        
        public ActionResult AddReview(int productId, FormCollection getReview)
        {
            EcommerceCustomerReviewDomain r = new EcommerceCustomerReviewDomain();
            r.CustomerId = CustomerHelper.CustomerId;
            r.ProductId = productId;
            r.Review = getReview["Review"];
            r.Email = getReview["email"];
            r.Rate = Convert.ToInt32(getReview["Rate"]);
            r.CreatedDate = DateTime.Now;
            r.IsPublished = false;
            r.StatementType = "Insert";
            DataAccess.EcommerceCustomerReview.InsertOrUpdate(r);
            return RedirectToAction("product/" + productId + "");
        }
    }
}