/*
 * File name            : CAppConfig.cs
  * Author               : Munirul Islam
 * Date                 : March 27, 2014
 * Version              : 1.0
 *
 * Description          :Application configaration
 *
 * Modification history:
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: Social Islami Bank Limited
 */
 
using System;
using System.Collections.Generic;
using System.Text;

namespace SIBLCommon.Common.Entity.Application
{
    [Serializable]
    public class CAppConfig
    {

        protected string m_sLogFile;
        protected string m_sLogLocation;
        protected int m_iLogging;
        protected int m_iDebug;
        protected string m_sHostAddress;
        protected string m_sMPVirtualDir;
        protected int m_iReceiverNameMaxLength;
        protected int m_iAccNoMaxLength;

        protected string m_sPlacidCommon;
        protected string m_sKMBCommon;
        protected string m_sAsiaCash;
        protected string m_sAsiaAccCredit;
        protected string m_sPKECash;
        protected string m_sPKEAccCredit;
        protected string m_sNECCash;
        protected string m_sNECAccCredit;
        protected string m_sUAECash;
        protected string m_sUAEAccCredit;
        protected string m_sDOHACash;
        protected string m_sDOHAAccCredit;
        protected string m_sLotus;
        protected string m_sAussieForex;
        protected string m_sPlacidCash;

        protected string m_sPasswordLogTimes;
        protected string m_sPasswordExpireDay;

        protected string m_sPartnerId;
        protected string m_sUserName;
        protected string m_sPassword;
        // disbursement credintial variable
        protected string m_sPartnerIdDisBursement;
        protected string m_sUserNameDisBursement;
        protected string m_sPasswordDisBursement;
        // end of disbursement credintial variable

        protected int m_iRefNoMaxLength;
        public CAppConfig()
        {
            m_sLogFile = String.Empty;
            m_sLogLocation = String.Empty;
            m_iLogging = 0;
            m_iDebug = 0;
            m_iReceiverNameMaxLength = 0;
            m_iAccNoMaxLength = 0;
            m_iRefNoMaxLength = 0;
            m_sHostAddress = String.Empty;
            m_sMPVirtualDir = String.Empty;

            m_sPlacidCommon = String.Empty;
            m_sKMBCommon = String.Empty;
            m_sAsiaCash = String.Empty;
            m_sAsiaAccCredit = String.Empty;
            m_sPKECash = String.Empty;
            m_sPKEAccCredit = String.Empty;
            m_sNECCash = String.Empty;
            m_sNECAccCredit = String.Empty;
            m_sUAECash = String.Empty;
            m_sUAEAccCredit = String.Empty;
            m_sDOHACash = String.Empty;
            m_sDOHAAccCredit = String.Empty;
            m_sLotus = String.Empty;
            m_sAussieForex = String.Empty;
            m_sPlacidCash = String.Empty;
            m_sPasswordLogTimes = String.Empty;
            m_sPasswordExpireDay = String.Empty;
            m_sPartnerId = string.Empty;
            m_sUserName = string.Empty;
            m_sPassword = string.Empty;

            //disbursement variable
            m_sPartnerIdDisBursement = string.Empty;
            m_sUserNameDisBursement = string.Empty;
            m_sPasswordDisBursement = string.Empty;
            // end of disbursement variable
        }

        #region Class properties  
   
        public string PartnerIdDisBursement
        {
            get { return m_sPartnerIdDisBursement; }
            set { m_sPartnerIdDisBursement = value; }
        }
        public string UserNameDisBursement
        {
            get { return m_sUserNameDisBursement; }
            set { m_sUserNameDisBursement = value; }
        }
        public string PasswordDisBursement
        {
            get { return m_sPasswordDisBursement; }
            set { m_sPasswordDisBursement = value; }
        }

        public int AccNoMaxLength
        {
            get { return m_iAccNoMaxLength; }
            set { m_iAccNoMaxLength = value; }
        }
        public int RefNoMaxLength
        {
            get { return m_iRefNoMaxLength; }
            set { m_iRefNoMaxLength = value; }
        }
        public int ReceiverNameMaxLength
        {
            get { return m_iReceiverNameMaxLength; }
            set { m_iReceiverNameMaxLength = value; }
        }

        public string HostAddress
        {
            get { return m_sHostAddress; }
            set { m_sHostAddress = value; }
        }

        public string MPVirtualDir
        {
            get { return m_sMPVirtualDir; }
            set { m_sMPVirtualDir = value; }
        }

        public string LogFile
        {
            get { return m_sLogFile; }
            set { m_sLogFile = value; }
        }

        public string LogLocation
        {
            get { return m_sLogLocation; }
            set { m_sLogLocation = value; }
        }
        public int Logging
        {
            get { return m_iLogging; }
            set { m_iLogging = value; }
        }
        
        public int Debug
        {
            get { return m_iDebug; }
            set { m_iDebug = value; }
        }


        #region Company Name



        #endregion Company Name

        public string PlacidCommon
        {
            get { return m_sPlacidCommon; }
            set { m_sPlacidCommon = value; }
        }
         public string KMBCommon
        {
            get { return m_sKMBCommon; }
            set { m_sKMBCommon = value; }
        }

         public string AsiaCash
        {
            get { return m_sAsiaCash; }
            set { m_sAsiaCash = value; }
        }
         public string AsiaAccCredit
        {
            get { return m_sAsiaAccCredit; }
            set { m_sAsiaAccCredit = value; }
        }

         public string PKECash
         {
             get { return m_sPKECash; }
             set { m_sPKECash = value; }
         }

        public string PKEAccCredit
        {
            get { return m_sPKEAccCredit; }
            set { m_sPKEAccCredit = value; }
        }
        public string NECCash 
        {
            get { return m_sNECCash; }
            set { m_sNECCash = value; }
        }
         public string NECAccCredit
        {
            get { return m_sNECAccCredit; }
            set { m_sNECAccCredit = value; }
        }

         public string UAECash
        {
            get { return m_sUAECash; }
            set { m_sUAECash = value; }
        }
         public string UAEAccCredit
        {
            get { return m_sUAEAccCredit; }
            set { m_sUAEAccCredit = value; }
        }
          public string DOHACash
        {
            get { return m_sDOHACash; }
            set { m_sDOHACash = value; }
        }
         public string DOHAAccCredit
        {
            get { return m_sDOHAAccCredit; }
            set { m_sDOHAAccCredit = value; }
        }

         public string Lotus
        {
            get { return m_sLotus; }
            set { m_sLotus = value; }
        }
         public string AussieForex
        {
            get { return m_sAussieForex; }
            set { m_sAussieForex = value; }
        }

         public string PlacidCash
        {
            get { return m_sPlacidCash; }
            set { m_sPlacidCash = value; }
        }
        

         public string PasswordLogTimes
         {
             get { return m_sPasswordLogTimes; }
             set { m_sPasswordLogTimes = value; }
         }

         public string PasswordExpireDay
         {
             get { return m_sPasswordExpireDay; }
             set { m_sPasswordExpireDay = value; }
         }

         public string PartnerId
         {
             get { return m_sPartnerId; }
             set { m_sPartnerId = value; }
         }

         public string UserName
         {
             get { return m_sUserName; }
             set { m_sUserName = value; }
         }

         public string Password
         {
             get { return m_sPassword; }
             set { m_sPassword = value; }
         }
  

        #endregion
    }
}
