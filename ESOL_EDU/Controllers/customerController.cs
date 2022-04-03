using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESOL_BO.Ecommerce;
using ESOL_BO.DbAccess;
using ESOL_EDU.Helper;
using System.Web.Security;

namespace ESOL_EDU.Controllers
{
    public class customerController : Controller
    {
        CommonHelper helper = new CommonHelper();

        public ActionResult Index()
        {
            ViewBag.OrderList = DataAccess.EcommerceOrder.GetByCustomerId(CustomerHelper.CustomerId);            
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(EcommerceCustomerDomain model)
        {
            if (model.UserName == null)
            {
                ViewBag.ErrorMessage = "Your user name is required";
            }
            else if (model.Password == null)
            {
                ViewBag.ErrorMessage = "Your password is required";
            }
            else
            {
                EcommerceCustomerDomain userInfo = DataAccess.EcommerceCustomer.Login(model);
                if (userInfo.UserName == model.UserName && userInfo.Password == model.Password)
                {
                    StoreSession(userInfo);
                    return RedirectToAction("Index", "customer");
                }
                else
                {
                    ViewBag.ErrorMessage = "Your user name or password is incorrect";
                }
            }
            return View();
        }
        public void StoreSession(EcommerceCustomerDomain domain)
        {
            Session["CustomerPortal"] = domain;
        }
        public ActionResult LogOff()
        {
            Session.Abandon();
            Session.Clear();
            HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            return RedirectToAction("Login", "customer");
        }
        public ActionResult Registation()
        {
            ViewBag.Message = TempData["SaveMessage"];
            return View();
        }
        [HttpPost]
        public ActionResult Registation(EcommerceCustomerDomain entity)
        {
            entity.UserName = entity.MobileNo;
            entity.Password = entity.Password;
            entity.Email = "info@tijori.com.bd";
            entity.GenderId = 0;
            entity.DateOfBirth = DateTime.Now;
            entity.VerifiedStatus = false;
            entity.IsActive = true;
            entity.CreatedBy = "ip";
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.StatementType = "Insert";
            DataAccess.EcommerceCustomer.InsertOrUpdate(entity);
            TempData["SaveMessage"] = "Your registration successfully";
            return RedirectToAction("Registation", "customer");
        }
        public int CheckMobile(string mobile)
        {
            try
            {
                var list = CommonHelper.CheckName("EcommerceCustomer", "where MobileNo='" + mobile + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CheckEmail(string email)
        {
            try
            {
                var list = CommonHelper.CheckName("EcommerceCustomer", "where Email='" + email + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult View(int id)
        {
            EcommerceOrderDomain list = DataAccess.EcommerceOrder.GetByCustomerInvoice(CustomerHelper.CustomerId,id);
            list.ItemList = DataAccess.EcommerceOrderDetails.GetByOrderId(id);
            list.Date = list.OrderDate.ToString("dd-MMM-yyyy");
            return View(list);
        }
        public JsonResult CustomerUpdate(EcommerceCustomerDomain customer)
        {
            customer.StatementType = "Update";
            customer.CustomerId = CustomerHelper.CustomerId;
            customer.UpdatedBy = Convert.ToString(CustomerHelper.CustomerId);
            return Json(DataAccess.EcommerceCustomer.InsertOrUpdate(customer), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeliveryAddressUpdate(EcommerceCustomerDomain customer)
        {
            customer.CustomerId = CustomerHelper.CustomerId;
            return Json(DataAccess.EcommerceCustomer.DeliveryAddressUpdate(customer), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangePassword(EcommerceCustomerDomain customer)
        {
            customer.CustomerId = CustomerHelper.CustomerId;
            return Json(DataAccess.EcommerceCustomer.ChangePassword(customer), JsonRequestBehavior.AllowGet);
        }
    }
}