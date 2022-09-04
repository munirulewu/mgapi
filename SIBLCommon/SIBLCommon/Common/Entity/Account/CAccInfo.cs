using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.Account
{
   [Serializable]
   public  class CAccInfo : ASIBLEntityBase
   {

       #region Protected Member       
       protected string m_sCustomerID;
       protected string m_sAccountName;
       protected string m_sAccountNumber;
       protected string m_sBalance;
       protected string m_sAccountType;
       protected string m_sStatus;
       protected string m_Userid;
       protected string m_sDate;
       protected string m_sGLID;
       protected string m_sTransactionID;
       protected string m_sParticulars;
       protected double m_sDrAmount;
       protected double m_sCrAmount;
       
       #endregion Protected Member
       #region Constructor
       public CAccInfo()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
       protected void Initialization()
       {
           m_sCustomerID = string.Empty;
           m_sAccountName = string.Empty;
           m_sAccountNumber = string.Empty;
           m_sBalance = string.Empty;
           m_sAccountType = string.Empty;
           m_sStatus = string.Empty;
           m_Userid = string.Empty;
           m_sTransactionID = string.Empty;
           m_sParticulars = string.Empty;
           m_sDate = string.Empty;
           m_sGLID = string.Empty;
           m_sDrAmount = 0;
           m_sCrAmount = 0;

       }
        #endregion Initialization
       #region Public Member

       public string CustomerID
       {
           get { return m_sCustomerID; }
           set { m_sCustomerID = value; }
       }

       public string UserId
       {
           get { return m_Userid; }
           set { m_Userid = value; }
       }

       public string AccountName
       {
           get { return m_sAccountName; }
           set { m_sAccountName = value; }
       }
       public string AccountNumber
       {
           get { return m_sAccountNumber; }
           set { m_sAccountNumber = value; }

       }
       public string Balance
       {
           get { return m_sBalance; }
           set { m_sBalance = value; }
       }
       public string AccountType
       {
           get { return m_sAccountType; }
           set { m_sAccountType = value; }
       }
       public string Status
       {
           get { return m_sStatus; }
           set { m_sStatus = value; }
       }
       public string TransactionDate
       {
           get { return m_sDate; }
           set { m_sDate = value; }
       }

       public string GLID
       {
           get { return m_sGLID; }
           set { m_sGLID = value; }
       }
       
       public double DrAmount
       {
           get { return m_sDrAmount; }
           set { m_sDrAmount = value; }

       }
       public double CrAmount
       {
           get { return m_sCrAmount; }
           set { m_sCrAmount = value; }
       }

       public string TransactionID
       {
           get { return m_sTransactionID; }
           set { m_sTransactionID = value; }
       }

       public string Particulars
       {
           get { return m_sParticulars; }
           set { m_sParticulars = value; }
       }


       #endregion Public Member
   }
}
