using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ESOL_BO.DbAccess
{
    public class ConnectionString
    {
        public static string connection = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        public static string GetConnection()
        {
            return connection;
        }
    }
}