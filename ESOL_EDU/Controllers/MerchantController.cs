using ESOL_BO.DbAccess;
using ESOL_BO.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;

namespace ESOL_EDU.Controllers
{
    public class MerchantController : Controller
    {
        public ActionResult Registration()
        {
            ViewBag.Message = TempData["SaveMessage"];
            return View();
        }
        [HttpPost]
        public ActionResult Registration(MerchantUserDomain entity, HttpPostedFileBase photo)
        {
            try
            {
                entity.OrgId = 1;
                entity.UserName = entity.Name;
                entity.Password = entity.Name;
                entity.IsAccept = true;
                entity.IsActive = true;
                entity.CreatedBy = entity.Name;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedBy = entity.Name;
                entity.UpdatedDate = DateTime.Now;
                byte[] data = null;
                if (photo != null)
                {
                    Stream inputStream = photo.InputStream;
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }
                entity.StudentPhoto = data;
                entity.StatementType = "Insert";
                DataAccess.MerchantUser.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Registration successfully";
                return RedirectToAction("Registration", "Merchant");
            }
            catch
            {
                return View();
            }
        }
    }
}