/*
 * File name            : COracleDatabaseFactory.cs
 * Author               : Munirul Islam
 * Date                 : March 24, 2014
 * Version              : 1.0
 *
 * Description          : The base class of database specific factory 
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Configuration;

namespace SIBLGlobalAPI.DA.Common.Connections
{
    /// <summary>
    /// The factory class of Oracle database
    /// </summary>
    public class COracleDatabaseFactory : ADatabaseBase 
    {        
        private static ADatabaseBase oFactory = null;

        /// <summary>
        /// Private for singleton
        /// </summary>
        private COracleDatabaseFactory()
        {           
        }

        /// <summary>
        /// The singleton factory method
        /// </summary>
        /// <returns></returns>
        public static ADatabaseBase GetOracleDatabaseFactory()
        {
            if (oFactory == null)
            {
                oFactory = new COracleDatabaseFactory();
            }

            return oFactory;
        }

        /// <summary>
        /// Create connections based on config data
        /// </summary>
        /// <returns></returns>
        protected override IDbConnection GetConnection()
        {
            IDbConnection oCnnection = null;
            try
            {
                /*CDatabaseConfiguration oConfig = CDatabaseUtil.GetConnectionConfig();
                string sConnectionString = oConfig.ConnectionString;*/

                var sConnectionString = ConfigurationManager.ConnectionStrings["ConStringAPP"].ConnectionString;
                oCnnection = new OracleConnection(sConnectionString);
                return oCnnection;
            }
            catch (OracleException oEx)
            {
                throw oEx;
            }
        }

        /// <summary>
        /// Opens the connection
        /// </summary>
        /// <returns></returns>
        public override IDbConnection OpenConnection()
        {
            IDbConnection oOraConnection = null;      
            try
            {
                oOraConnection = (OracleConnection)GetConnection();
                oOraConnection.Open();                
            }
            catch (OracleException oEx)
            {
                Console.WriteLine("Error: {0}", oEx.Message);
            }

            return oOraConnection;
        }

        /// <summary>
        /// Close an open connection
        /// </summary>
        /// <param name="oDBConnection"></param>
        public override void CloseConnection(IDbConnection oDBConnection)
        {
            if (oDBConnection.State != ConnectionState.Closed)
            {
                oDBConnection.Close();
                oDBConnection.Dispose();
            }
        }

        /// <summary>
        /// Begins the transaction for create and update operations
        /// </summary>
        /// <param name="oConnection"></param>
        /// <returns></returns>
        public override IDbTransaction BeginTransaction(IDbConnection oConnection)
        {
            IDbTransaction oTransaction = null;
            try
            {
                oTransaction = oConnection.BeginTransaction();                
            }
            catch (OracleException oEx)
            {
                throw oEx;
            }

            return oTransaction;
        }

        /// <summary>
        /// Data reader for executing SQL string passed manually
        /// </summary>
        /// <param name="oCommand"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public override IDataReader ExecuteSQLDataReader(IDbCommand oCommand, string commandText)
        {           
            OracleDataReader oDataReader = null;

            try
            {     
                oDataReader = (OracleDataReader)oCommand.ExecuteReader();
            }

            catch (OracleException oEx)
            {                 
                throw oEx;
            }

            return oDataReader;
        }

        /// <summary>
        /// DataSet for executing SQL string passed manually
        /// </summary>
        /// <param name="oConnection"></param>
        /// <param name="sCommandText"></param>
        /// <returns></returns>
        public override DataSet ExecuteSQLDataSet(IDbConnection oConnection, string sCommandText)
        {
            OracleDataAdapter oDataAdapter = new OracleDataAdapter(sCommandText, (OracleConnection)oConnection);
            DataSet oDataSet = new DataSet();

            try
            {
                oDataAdapter.Fill(oDataSet);
            }
            catch (OracleException oEx)
            {                
                throw oEx;
            }

            return oDataSet;
        }

        /// <summary>
        /// DataSet for executing sql command
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <returns></returns>
        public override DataSet ExecuteDataSet(IDbCommand oDbCommand)
        {
            OracleDataAdapter oDataAdapter = new OracleDataAdapter((OracleCommand) oDbCommand);
            DataSet oDataSet = new DataSet();

            try
            {
                oDataAdapter.Fill(oDataSet);
            }
            catch (OracleException oEx)
            {                
                throw oEx;
            }

            return oDataSet;
        }

        /// <summary>
        /// Add/update data in the database for the SQL string passed manually
        /// </summary>
        /// <param name="oCommand"></param>
        /// <param name="sCommandText"></param>
        /// <returns></returns>
        public override int ExecuteSQLNonReader(IDbCommand oCommand, string sCommandText)
        {
            int iRetVal = 0;
            OracleCommand oOraCommand = (OracleCommand)oCommand;
            try
            {               
                oOraCommand.CommandText = sCommandText;
                iRetVal = oOraCommand.ExecuteNonQuery();     
                return iRetVal;
            }
            catch(OracleException oEx)
            {
                throw oEx;
            }
            //finally
            //{
            //    oOraCommand.Dispose();
            //}
        }

        /// <summary>
        /// Data reader for executing sql command
        /// </summary>
        /// <param name="oCommand"></param>
        /// <returns></returns>
        public override IDataReader ExecuteReader(IDbCommand oCommand)
        {
            OracleCommand oOracleCommand = (OracleCommand)oCommand;  
            OracleDataReader oDataReader = null;

            try
            {
                oDataReader = oOracleCommand.ExecuteReader();               
                return oDataReader;
            }
            catch (OracleException oEx)
            {
                throw oEx;
            }
            //finally
            //{
            //    oOracleCommand.Dispose();
            //}
        }

        /// <summary>
        /// Add/update operation for executing sql command
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <returns></returns>
        public override int ExecuteNonReader(IDbCommand oDbCommand)
        {
            int iRetVal = 0;
            OracleCommand oOraCommand = (OracleCommand)oDbCommand;
            try
            {                
                iRetVal = oOraCommand.ExecuteNonQuery();
                return iRetVal;
            }
            catch (OracleException oEx)
            {
                throw oEx;
            }
            //finally
            //{
            //    oOraCommand.Dispose();
            //}
        }

    }
}
