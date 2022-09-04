/*
 * File name            : CConstants.cs
 * Author               : Munirul Islam
 * Date                 : March 27,2014
 * Version              : 1.0
 *
 * Description          : Constant Class for literals
 *
 * Modification history :
 * Name                         Date                            Desc
 *                                          
 * 
 * Copyright (c) 2014: Social Islami Bank Limited
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SIBLCommon.Common
{     
    public static class CConstants
    {
         
        //#region ResponseCodeandMessage

        //public const string responseCode0000 = "0000";
        //public const string messageforCode0000 = "Success";
        //public const string responseCode9999 = "9999";
        //public const string messageforCode9999 = "Fail";


        //public const string responseCode1001 = "1001";
        //public const string messageforCode1001 = "{PropertyName} can not be empty";
        //public const string responseCode1002 = "1002";
        //public const string messageforCode1002 = "{PropertyName} minimum character length 1";
        //public const string responseCode1003 = "1003";
        //public const string messageforCode1003 = "{PropertyName} minimum character length 5";
        //public const string responseCode1004 = "1004";
        //public const string messageforCode1004 = "{PropertyName} must be 4 characters";
        //public const string responseCode1005 = "1005";
        //public const string messageforCode1005 = "{PropertyName} maximum character length 100";
        //public const string responseCode1006 = "1006";
        //public const string messageforCode1006 = "{PropertyName} must be valid number";
        //public const string responseCode1007 = "1007";
        //public const string messageforCode1007 = "Invalid {PropertyName} value";
        //public const string responseCode1008 = "1008";
        //public const string messageforCode1008 = "{PropertyName} maximum character length 10";
        //public const string responseCode1009 = "1009";
        //public const string messageforCode1009 = "{PropertyName} must be valid date";
        //public const string responseCode1010 = "1010";
        //public const string messageforCode1010 = "{PropertyName} minimum character length 1";
        //public const string responseCode1011 = "1011";
        //public const string messageforCode1011 = "{PropertyName} maximum character length 20";
        //public const string responseCode1012 = "1012";
        //public const string messageforCode1012 = "{PropertyName} maximum character length 30";


        //public const string responseCode2001 = "2001";
        //public const string messageforCode2001 = "You can not add duplicate information";
        //public const string responseCode2002 = "2002";
        //public const string messageforCode2002 = "Transaction is completed";
        //public const string responseCode2003 = "2003";
        //public const string messageforCode2003 = "Transaction is pending";
        //public const string responseCode2004 = "2004";
        //public const string messageforCode2004 = "Transaction is not found in SIBL";
        //public const string responseCode2005 = "2005";
        //public const string messageforCode2005 = "Invalid company code. Please provide correct company code provided by SIBL";
        //public const string responseCode2006 = "2006";
        //public const string messageforCode2006 = "Invalid user name. Please provide correct user name provided by SIBL";
        //public const string responseCode2007 = "2007";
        //public const string messageforCode2007 = "Invalid password. Please provide correct password provided by SIBL";
        //public const string responseCode2008 = "2008";
        //public const string messageforCode2008 = "Transaction does not exist";

        //public const string responseCode3001 = "3001";
        //public const string messageforCode3001 = " invalid json token name. ";





        //#endregion

        //#region RegulerExpression

        //public const string regExpDateFormat = @"((18|19|20)[0-9]{2}[\-.](0[13578]|1[02])[\-.](0[1-9]|[12][0-9]|3[01]))|(18|19|20)[0-9]{2}[\-.](0[469]|11)[\-.](0[1-9]|[12][0-9]|30)|(18|19|20)[0-9]{2}[\-.](02)[\-.](0[1-9]|1[0-9]|2[0-8])|(((18|19|20)(04|08|[2468][048]|[13579][26]))|2000)[\-.](02)[\-.]29$";
        ////(((19|20)([2468][048]|[13579][26]|0[48])|2000)[/-]02[/-]29|((19|20)[0-9]{2}[/-](0[4678]|1[02])[/-](0[1-9]|[12][0-9]|30)|(19|20)[0-9]{2}[/-](0[1359]|11)[/-](0[1-9]|[12][0-9]|3[01])|(19|20)[0-9]{2}[/-]02[/-](0[1-9]|1[0-9]|2[0-8])))
        //public const string regExpNumber = @"^[0-9]*$";

        //#endregion

        #region JsonConstant

        public const string DISTRICT_NAME_LIST = "DistrictList";
        public const string COUNTRY_NAME_LIST = "CountryList";
        public const string BANK_NAME_LIST = "BankList";
        public const string BRANCH_NAME_LIST = "BranchList";
        public const string ERROR_NAME_LIST = "ErrorList";

        #endregion

        #region Enum Text

        public enum TransactionTypeForTwoPercentIncentive
        {
            CASH = 1, OWNBANK = 2, BEFTN = 3
        }

        public enum DisbursementType
        {
            OWN_BANK = 1,
            OTHER_BANK = 2
        }

        
        #endregion

        #region Database
        public const string ACTIVATE = "ACTIVATE";
        public const string DEACTIVATE = "DEACTIVATE";
        public const string CLOSE = "CLOSE";
        public const string RESEND = "RESEND";
        public const string DEFAULT_IN = "Get";
        public const string NEW_ACCOUNT = "N";
        public const string ACTIVE_ACCOUNT = "A";
        public const string PREFERRED_ACCOUNT = "P";
        public const string DELETED_CLOSED_ACCOUNT = "D";
        public const string SUSPENDED_ACCOUNT = "S";
        public const string RESEND_ACCOUNT = "R";
        public const string DB_SUCCESS = "1";
        public const string DB_UNSUCCESS = "2";
        public const string DB_ERROR = "3";
        public const string DB_ADD = "ADD";
        public const string DB_UPDATE = "UPDATE";
        public const string DB_DELETE = "DELETE";
        public const string DB_SUBMIT = "SUBMITTED";
        public const string DB_APPROVED = "APPROVED";
        public const string DB_APPROVED_VALUE = "1";
        public const string DB_EXIST = "E";
        public const string DB_SET_PAYMENT_MODE = "SET_PAYMENT_MODE";
        public const string GET_ALL = "GetAll";
        public const string DB_GENERATECHEQUE = "GENERATECHEQUE";
        public const string DB_REQUEST = "REQUEST";
        public const string DB_ADD_CHECK = "ADDCHECK";
        public const string DB_REMOVE = "REMOVE";

        public const string DB_LISTTYPE = "LIST";
        public const string DB_DATASET = "DS";

        #endregion Database

        #region Error Report


        #endregion Error Report


        #region Button Text

        public const string BUTTON_SAVE_TEXT = "Save";
        public const string BUTTON_UPDATE_TEXT = "Update";
        public const string BUTTON_SEARCH_TEXT = "Search";
        public const string BUTTON_DELETE_TEXT = "Delete";
        public const string BUTTON_EXPORT_TEXT = "Export";
        public const string BUTTON_UPLOAD_TEXT = "Upload";

        #endregion
        

        #region Configuration File Location
        public const string REQUEST_FILE = @"config\Request.config";
        public const string STATIC_DATA_FILE = @"config\StaticData.config";
        public const string TOOLTIP_DATA_FILE = @"config\TooltipText.config";
        #endregion Configuration File Location

        #region Caching constants
        //For XML data file caching as well as cache key
        public const String CSREQMAPPINGXMLDATA = "RequestMapping";

        public const String CSAPPCONFIGXMLDATA = "AppConfig";

        public const String CSCACHEMAPPINGXMLDATA = "CacheMapping";

        #endregion

        #region Role Permission




        // for cacheing
        public const string ROLEPER_CACHE_ROLELIST = "RoleList";

        #endregion

        #region Messages
        //public const string SERVER_ERROR_MESSAGE = "Internal server error.Please try later.";
        #endregion Messages

        #region request Name
        public const string GET_PAYMENT_MODE = "GET_PAYMENT_MODE";

        public const string GET_DISTRICT = "GetDistrictList";
        public const string GET_DISTRICT_BY_BANK = "GetDistrictListByBank";
        public const string GET_BRANCH_LIST = "GetBranchList";
        public const string GET_BANK_LIST = "GetBankList";
        public const string GET_BRANCHLIST_BY_DISTRICT = "GetDistrictBranchList";
        public const string GET_COMMON_ITEM_LIST = "CommonSelectItem";

        public const string GET_ROLE_PERMISSION_LIST = "GetRolePermissionList";
        public const string GET_PERMISSION_LIST = "GetPermissionList";
        public const string GET_FILE_LIST = "GetCompanyFileName";
        public const string GET_USER = "GetUserList";
        public const string GET_BRANCH_LIST_BY_DATA = "GetBranchListbyData";
        public const string GET_LOCATION = "GetLocation";

        public const string GET_REMITINFO = "GetRemitInfo";
        public const string GET_REMITINFO_REPORT = "GetRemitInfoReport";

        #endregion

        #region SESSION VARIABLE
        public const string LOGIN_SESSION = "LOGIN_SESSION";
        public const string BRANCH_SESSION = "BRANCH_SESSION";
        public const string BANK_SESSION = "BANK_SESSION";
        public const string USER_SESSION = "USER_SESSION";
        public const string ROLE_SESSION = "ROLE_SESSION";
        public const string LOAD_USER_SESSION = "USER_SESSION_load";
        public const string USER_SESSION_CREATE = "CREATE_USER_SESSION";
        public const string USER_SESSION_DETAILS = "DETAILS_USER_SESSION";
        public const string CREATE_ROLE_SESSION = "RoleSessionObj";
        public const string REPORT_PARAMETER = "REPORT_PARAMETER";
        public const string PAGE_SESSION_MISMATCHED_BANK = "PAGE_SESSION_MISMATCHED_BANK";
        public const string PAGE_SESSION_SEARCHING_DATE = "PAGE_SESSION_SEARCHING_DATE";
        public const string REPORT_SESSION = "REPORT_SESSIOM";

        public const string SESSION_LOGIN_USEROBJECT = "user";

        #endregion SESSION VARIABLE

        #region page names
        public const string INDEX_PAGE = "~/Default.aspx";
        public const string INDEX_LOGIN = "~/Pages/UserLogin.aspx";
        public const string PAGE_DASHBOARD = "~/Pages/Dashboard/Dashboard.aspx?id=t";
        public const string PAGE_CHANGE_PASSWORD_URL = "~/Pages/User/ChangePassword.aspx";
        public const string PAGE_CREATE_USER_URL = "~/Pages/User/CreateUser.aspx";
        public const string PAGE_USER_DETAILS_URL = "~/Pages/User/UserDetails.aspx";
        public const string PAGE_USER_LIST_URL = "~/Pages/User/UserList.aspx";

        #endregion page names

        #region page key

        public const string PAGE_BBANK_EXPORT_DATA = "BB_REPORT_EXPORT";
        public const string PAGE_CHANGE_PASSWORD = "CHANGE_PASSWORD";
        public const string PAGE_BANK_INFO = "BANK_INFO";
        public const string PAGE_SEARCH_BRANCH_INFO = "SEARCH_BRANCH_INFO";
        public const string PAGE_ROLE_PERMISSION = "ROLE_PERMISSION";
        public const string PAGE_USER_CREATE_INFO = "USER_CREATE";
        public const string PAGE_ROLE_CREATE_INFO = "ROLE_CREATE";

        public const string PAGE_CHECKER_INFO = "CHECKER_INFO";
        public const string PAGE_USER_LOCKED_UNLOCKED = "USER_LOCKED_UNLOCKED";
        public const string PAGE_BRANCH_INFO = "BRANCH_INFO";
        public const string PAGE_ROLE_INFO = "ROLE_INFO";
        public const string PAGE_PROCESS_DATA = "PROCESS_DATA";
        public const string PAGE_USER_LIST = "USER_LIST";
        public const string PAGE_USER_DETAILS = "USER_DETAILS";
        public const string PAGE_FILE_DELETE = "DELETE_FILE";

        //public const string PAGE_GET_BILLDETAILS = "GET_BILLDETAILS";
        public const string PAGE_POSTBILL = "POST_BILL";
        public const string PAGE_REVERSEBILL = "REVERSE_BILL";
        public const string PAGE_SIBLSGCLREPORTS = "SIBLSGCL_REPORTS";
        public const string PAGE_SIBLSGCLSIBL_REPORTS = "SIBLSGCL_SIBL_REPORTS";
        public const string PAGE_REPORTVIEWER = "REPORT_VIEWER";
        public const string PAGE_RPT_SHOW_TRANSACTION = "RPT_SHOW_TRANSACTION";

        #endregion page key

        #region Table Key

        public const string PLACID_CASH_PAYMENT = "PLACID_CASH_PAYMENT";
        public const string PLACID_COMMON = "PLACID_TABLE";
        public const string KMB_COMMON = "KMB_TABLE";
        public const string ASIA_CASH = "ASIA_CASH_TABLE";
        public const string ASIA_ACC_CREDIT = "ASIA_ACCOUNT_CREDIT_TABLE";
        public const string PKE_CASH = "PKE_CASH_TABLE";
        public const string PKE_ACC_CREDIT = "PKE_ACCOUNT_CREDIT_TABLE";
        public const string NEC_CASH = "NEC_CASH_TABLE";
        public const string NEC_ACC_CREDIT = "NEC_ACCOUNT_CREDIT_TABLE";
        public const string UAE_CASH = "UAE_CASH_TABLE";
        public const string UAE_ACC_CREDIT = "UAE_ACCOUNT_CREDIT_TABLE";
        public const string DOHA_CASH = "DOHA_CASH_TABLE";
        public const string DOHA_ACC_CREDIT = "DOHA_ACCOUNT_CREDIT_TABLE";
        public const string LOTUS_COMMON = "LOTUS_TABLE";
        public const string FOREX_COMMON = "FOREX_TABLE";
        #endregion Table key


    }
}
