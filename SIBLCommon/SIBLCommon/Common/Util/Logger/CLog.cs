/**
 * File name            : CLog.cs
 * Author               : Munirul Islam
 * Date                 : March 27, 2014
 * Version              : 1.0
 *
 * Description          : All kind of XML file manipulator
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
using System.IO;
using SIBLCommon.Common.Util.Configuration;
using SIBLCommon.Common.Entity.Application;


namespace SIBLCommon.Common.Util.Logger
{
   /**
    * This class acts as an utility to write log for different purposes from entire application. 
    * The log writing is an asynchronous process. 
    **/
    public class CLog
    {
        //Static constant to dfine log type to be used in other class
        public const String INFORMATION = "INF";
        public const String SUCCESS = "SCS";
        public const String ERROR = "ERR";
        public const String EXCEPTION = "EXP";
        public const String DEBUG = "DBG";

        //The log configuration information data model
        protected static CAppConfig m_oConfig = null;

        //Define the method type that will be called
        private delegate void WriteDelegate(String sDataBuffer);

        private static CLog m_oLog;
        
       /**
        * The constuctor
        * 
        * */
        private CLog()
        {
            
        }


        /**
        * The constuctor
        * 
        * */
        public static CLog Logger
        {
            get
            {
                if (m_oLog == null)
                {
                    m_oLog = new CLog();                    
                }             
                return m_oLog;
            }
        }




       /**
        * Public API to write log in the file location
        **/

        public void Write(String sInfoType, String sData)
        {
            //Load the file location for log configuration
            if (CLog.m_oConfig == null)
            {
                String sPath = System.AppDomain.CurrentDomain.BaseDirectory;
                CLog.m_oConfig = CXMLDataManager.GetAppConfigObject();
            }
            
          
            //Check whether data is empty
            if (sData.Trim().Equals("") || sData == null || sData == String.Empty)
            {
                return;
            }           

          

            //Format the log string prefix depends on time
            DateTime dtNow = DateTime.Now;
            String sDateTimeString = dtNow.Date.ToString() + " " + dtNow.TimeOfDay.ToString();

            //Call the delegate function
            WriteWithDelegate("Log: " + dtNow.ToString() + " - " + sInfoType + " :: " + sData);
        }


       /**
        * Writes the log data depem\nds on delegate declared
        **/
        protected static void WriteWithDelegate(String sData)
        {
            //Locate the logger file
            DateTime dtNow = DateTime.Now;
            string sFileName = dtNow.Day.ToString() + "_" + dtNow.Month + "_" + dtNow.Year.ToString();
            String sFilePath = CLog.m_oConfig.LogLocation + @"\" + CLog.m_oConfig.LogFile+sFileName + ".log";

            //Locate the logger backup file
            String sCopiedPath = CLog.m_oConfig.LogLocation + @"\" + CLog.m_oConfig.LogFile + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".log";
            /*
            if (File.Exists(sFilePath))
            {
                FileInfo oFileInfo = new FileInfo(sFilePath);
                long lByte = oFileInfo.Length;

                //Backup the log file
                if (lByte >= (1024 * 50))
                {
                    lock (sFilePath)
                    {
                        File.Copy(sFilePath, sCopiedPath);
                        File.Delete(sFilePath);
                    }
                }
            }
             */ 
            // create a new WriteDelegate
            WriteDelegate oDelegate = new WriteDelegate(WriteLogData);

            // Invoke the delegate asynchronously.
            IAsyncResult oAsyncRes = oDelegate.BeginInvoke(sData, null, null);

            // WriteTheData is now executing asynchronously
            // Continue with foreground processing here.
            while (!oAsyncRes.IsCompleted)
            {
                Console.Write('.');
                System.Threading.Thread.Sleep(10);
            }
            Console.WriteLine();

            // harvest the result
            oDelegate.EndInvoke(oAsyncRes);
        }

       /**
        * Tyh writer methiods registered in delegate
        **/
        protected static void WriteLogData(String dataBuffer)
        {
            try
            {
                if(!Directory.Exists(CLog.m_oConfig.LogLocation))
                {
                    Directory.CreateDirectory(CLog.m_oConfig.LogLocation);
                }

                //Formate the file with its extension
                DateTime dtNow = DateTime.Now;
                string sFileName = dtNow.Day.ToString() + "_" + dtNow.Month + "_" + dtNow.Year.ToString();
                String sFilePath = CLog.m_oConfig.LogLocation + @"\" + CLog.m_oConfig.LogFile + sFileName + ".log";
                using (StreamWriter oStreamWriter = File.AppendText(sFilePath))
                {
                    oStreamWriter.WriteLine(dataBuffer);
                    oStreamWriter.Close();                  
                }                
            }
            catch(Exception ex)
            {
                throw ex;
                //Console.Write(ex.Message);

            }
        }
    }
}
