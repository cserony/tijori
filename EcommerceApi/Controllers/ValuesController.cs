using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ESOL_BO.Ecommerce;
using ESOL_BO.DbAccess;

namespace EcommerceApi.Controllers
{
    public class ValuesController : ApiController
    {
        #region GeneralNotice
        [HttpGet]
        [Route("GeneralNotice")]
        public IHttpActionResult GeneralNotice()
        {
            List<EcommerceNoticeDomain> oNotice = new List<EcommerceNoticeDomain>();
            try
            {
                oNotice = DataAccess.EcommerceNotice.GetByOrg("select top 1 * from EcommerceNotice order by NoticeId desc");
                var ob = (from q in oNotice
                          select new
                          {
                              NoticeId = q.NoticeId,
                              Title = q.NoticeName,
                              Description = q.Description,
                          }).ToList();
                return Ok(ob);
            }
            catch (Exception ex)
            {
                return Ok("method error:" + ex.Message.ToString());
            }
        }
        #endregion

    }
}
