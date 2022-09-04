using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace SIBLMGAPI.Models
{
    public class CValidateUser
    {
        public static bool Login(string username, string password)
        {
            bool bRetValue = false;
            string apiUserName=ConfigurationManager.AppSettings["apiusername"].ToString();
            string apiPassword = ConfigurationManager.AppSettings["apipassword"].ToString();
            if (apiUserName == username && apiPassword == password)
                bRetValue = true;
            else
                bRetValue = false;
            return bRetValue;
            
        }
    }
}