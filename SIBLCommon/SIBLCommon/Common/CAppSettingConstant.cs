using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common
{
    public class CAppSettingConstant
    {

        #region ResponseCodeandMessage
        
        public string responseCode0000 = ConfigurationManager.AppSettings["responseCode0000"];
        public string messageforCode0000 = ConfigurationManager.AppSettings["messageforCode0000"];
        public string responseCode9999 = ConfigurationManager.AppSettings["responseCode9999"];
        public string messageforCode9999 = ConfigurationManager.AppSettings["messageforCode9999"];
        public string responseCode8888 = ConfigurationManager.AppSettings["responseCode8888"];
        public string messageforCode8888 = ConfigurationManager.AppSettings["messageforCode8888"];   
                
        public string responseCode1001 = ConfigurationManager.AppSettings["responseCode1001"];
        public string messageforCode1001 = ConfigurationManager.AppSettings["messageforCode1001"];
        public string responseCode1002 = ConfigurationManager.AppSettings["responseCode1002"];
        public string messageforCode1002 = ConfigurationManager.AppSettings["messageforCode1002"];
        public string responseCode1003 = ConfigurationManager.AppSettings["responseCode1003"];
        public string messageforCode1003 = ConfigurationManager.AppSettings["messageforCode1003"];
        public string responseCode1004 = ConfigurationManager.AppSettings["responseCode1004"];
        public string messageforCode1004 = ConfigurationManager.AppSettings["messageforCode1004"];
        public string responseCode1005 = ConfigurationManager.AppSettings["responseCode1005"];
        public string messageforCode1005 = ConfigurationManager.AppSettings["messageforCode1005"];
        public string responseCode1006 = ConfigurationManager.AppSettings["responseCode1006"];
        public string messageforCode1006 = ConfigurationManager.AppSettings["messageforCode1006"];
        public string responseCode1007 = ConfigurationManager.AppSettings["responseCode1007"];
        public string messageforCode1007 = ConfigurationManager.AppSettings["messageforCode1007"];
        public string responseCode1008 = ConfigurationManager.AppSettings["responseCode1008"];
        public string messageforCode1008 = ConfigurationManager.AppSettings["messageforCode1008"];
        public string responseCode1009 = ConfigurationManager.AppSettings["responseCode1009"];
        public string messageforCode1009 = ConfigurationManager.AppSettings["messageforCode1009"];
        //public string responseCode1010 = ConfigurationManager.AppSettings["responseCode1010"];
        //public string messageforCode1010 = ConfigurationManager.AppSettings["messageforCode1010"];
        public string responseCode1011 = ConfigurationManager.AppSettings["responseCode1011"];
        public string messageforCode1011 = ConfigurationManager.AppSettings["messageforCode1011"];
        public string responseCode1012 = ConfigurationManager.AppSettings["responseCode1012"];
        public string messageforCode1012 = ConfigurationManager.AppSettings["messageforCode1012"];
        
                
        public string responseCode3001 = ConfigurationManager.AppSettings["responseCode3001"];
        public string messageforCode3001 = ConfigurationManager.AppSettings["messageforCode3001"];

        #endregion


        #region RegulerExpression
        
        public string regExpDateFormat = ConfigurationManager.AppSettings["regExpDateFormat"];        
        public string regExpNumber = ConfigurationManager.AppSettings["regExpNumber"];

        #endregion

    }
}
