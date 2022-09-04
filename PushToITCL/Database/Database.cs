/*
 * File name            : Database.cs
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
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Globalization;

using System.Data.OleDb;
namespace PushDataMGoITCL
{

    /// <summary>
    /// The main class for database operations
    /// </summary>
    public class Database
    {
        private const string DBORACLE = "Oracle";
        private static string m_sCurrentDB = null;
        private IDbConnection m_oConnection = null;  

        /// <summary>
        /// Singleton method to know default database type
        /// </summary>
        /// <returns></returns>
        protected string GetDBType()
        {
            m_sCurrentDB = DBORACLE;
            //if (m_sCurrentDB == null)
            //{    
            //    CDatabaseConfiguration oConfig = CDatabaseUtil.GetConnectionConfig();
            //    m_sCurrentDB = oConfig.Default;                
            //}

            return m_sCurrentDB;
        }

        /// <summary>
        /// Used as or check OracleDbType as "RefCursor"
        /// </summary>
        public static string RefCursor
        {
            get
            {
                return "RefCursor";
            }
        }


        /// <summary>
        /// Builds a value parameter name for the current database.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>A correctly formated parameter name.</returns>
        public virtual string BuildParameterName(string name)
        {
            return name;
        }


        /// <summary>
        /// Singleton method to create the database connection using factory
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection( string sConnName)
        {
            
            try
            {
                m_oConnection = COracleDatabaseFactory.GetOracleDatabaseFactory().OpenConnection(sConnName);
               
            }             
            catch(Exception dEx)
            {
                throw dEx;
            }
            return m_oConnection;
        }        
       
        /// <summary>
        /// Global method for adding stored procedure parameter
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <param name="sName"></param>
        /// <param name="oDbType"></param>
        /// <param name="iSize"></param>
        /// <param name="oDirection"></param>
        /// <param name="bNullable"></param>
        /// <param name="byPrecision"></param>
        /// <param name="byScale"></param>
        /// <param name="sSourceColumn"></param>
        /// <param name="oSourceVersion"></param>
        /// <param name="oValue"></param>
        public virtual void AddParameter(IDbCommand oDbCommand, string sName, DbType oDbType, int iSize, ParameterDirection oDirection, bool bNullable, byte byPrecision, byte byScale, string sSourceColumn, DataRowVersion oSourceVersion, object oValue)
        {
            if (GetDBType().Equals(DBORACLE))
            {
                OracleParameter oParameter = new OracleParameter();               

                oParameter.DbType = oDbType;
                oParameter.ParameterName = sName;
                oParameter.Size = iSize;
                oParameter.Direction = oDirection;
                oParameter.IsNullable = bNullable;
                oParameter.Precision = byPrecision;
                oParameter.Scale = byScale;
                oParameter.SourceColumn = sSourceColumn;
                oParameter.SourceVersion = oSourceVersion;
                oParameter.Value = oValue;

                ((OracleCommand)oDbCommand).Parameters.Add(oParameter);
            }           
        }


        /// <summary>
        /// Global method for adding stored procedure parameter
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <param name="sName"></param>
        /// <param name="oDbType"></param>
        /// <param name="iSize"></param>
        /// <param name="oDirection"></param>
        /// <param name="bNullable"></param>
        /// <param name="byPrecision"></param>
        /// <param name="byScale"></param>
        /// <param name="sSourceColumn"></param>
        /// <param name="oSourceVersion"></param>
        /// <param name="oValue"></param>
        public virtual void AddParameter(IDbCommand oDbCommand, string sName, OracleDbType oDbType, int iSize, ParameterDirection oDirection, bool bNullable, byte byPrecision, byte byScale, string sSourceColumn, DataRowVersion oSourceVersion, object oValue)
        {
            if (GetDBType().Equals(DBORACLE))
            {
                OracleParameter oParameter = new OracleParameter();

                oParameter.ParameterName = sName;
                oParameter.OracleDbType = oDbType;
                oParameter.Size = iSize;
                oParameter.Direction = oDirection;
                oParameter.IsNullable = bNullable;
                oParameter.Precision = byPrecision;
                oParameter.Scale = byScale;
                oParameter.SourceColumn = sSourceColumn;
                oParameter.SourceVersion = oSourceVersion;
                oParameter.Value = oValue;           

                ((OracleCommand)oDbCommand).Parameters.Add(oParameter);
            }
        }


        /// <summary>
        /// Global method for adding stored procedure parameter
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <param name="sName"></param>
        /// <param name="oDbType"></param>
        /// <param name="oDirection"></param>
        /// <param name="sSourceColumn"></param>
        /// <param name="oSourceVersion"></param>
        /// <param name="oValue"></param>
        public virtual void AddParameter(IDbCommand oDbCommand, string sName, DbType oDbType, ParameterDirection oDirection, string sSourceColumn, DataRowVersion oSourceVersion, object oValue)
        {
            if (GetDBType().Equals(DBORACLE))
            {             
                OracleParameter oParameter = new OracleParameter();

                oParameter.ParameterName = sName;
                oParameter.DbType = oDbType;
                oParameter.Direction = oDirection;
                oParameter.SourceColumn = sSourceColumn;
                oParameter.SourceVersion = oSourceVersion;
                oParameter.Value = oValue;
                
                ((OracleCommand)oDbCommand).Parameters.Add(oParameter);
            }
        }

        /// <summary>
        /// Global method for adding stored procedure 'out' parameter
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <param name="sName"></param>
        /// <param name="oDbType"></param>
        /// <param name="iSize"></param>
        public virtual void AddOutParameter(IDbCommand oDbCommand, string sName, object oDbType, int iSize)
        {
            if (GetDBType().Equals(DBORACLE))
            {
                OracleParameter oParameter = new OracleParameter();

                if (oDbType.ToString().Equals(Database.RefCursor) || (oDbType.GetType().Equals(typeof(OracleDbType))))
                {
                    oParameter.OracleDbType = OracleDbType.RefCursor;
                }
                else if (oDbType.GetType().Equals(typeof(DbType)))
                {
                    oParameter.DbType = (DbType)oDbType;
                }

                oParameter.ParameterName = sName;
                oParameter.Size = iSize;
                oParameter.Direction = ParameterDirection.Output;
                oParameter.IsNullable = true;
                oParameter.Precision = 0;
                oParameter.Scale = 0;
                oParameter.SourceColumn = String.Empty;
                oParameter.SourceVersion = DataRowVersion.Default;
                oParameter.Value = DBNull.Value;

                ((OracleCommand)oDbCommand).Parameters.Add(oParameter);
            }
            else
            {
                AddParameter(oDbCommand, sName, (DbType)oDbType, iSize, ParameterDirection.Output, true, 0, 0, String.Empty, DataRowVersion.Default, DBNull.Value);
            }
        }     

        /// <summary>
        /// Global method for adding stored procedure 'in' parameter
        /// </summary>
        /// <param name="oCommand"></param>
        /// <param name="sName"></param>
        /// <param name="oDbType"></param>
        public void AddInParameter(IDbCommand oCommand, string sName, DbType oDbType)
        {
            AddParameter(oCommand, sName, oDbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, null);
        }

        /// <summary>
        /// Global method for adding stored procedure 'in/out' parameter
        /// </summary>
        /// <param name="oCommand"></param>
        /// <param name="sName"></param>
        /// <param name="oDbType"></param>
        /// <param name="iSize"></param>
        /// <param name="oValue"></param>
        public void AddInOutParameter(IDbCommand oCommand, string sName, DbType oDbType, int iSize, object oValue)
        {
            AddParameter(oCommand, sName, oDbType, iSize, ParameterDirection.InputOutput, false, 0, 0, sName, DataRowVersion.Default, oValue);
        }

       
        /// <summary>
        /// Global method for adding stored procedure 'in' parameter
        /// </summary>
        /// <param name="oCommand"></param>
        /// <param name="sName"></param>
        /// <param name="oDbType"></param>
        /// <param name="oValue"></param>
        public void AddInParameter(IDbCommand oCommand, string sName, DbType oDbType, object oValue)
        {
            AddParameter(oCommand, sName, oDbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, oValue);
        }

        /// <summary>
        /// Global method for adding stored procedure 'in' parameter
        /// </summary>
        /// <param name="oCommand"></param>
        /// <param name="sName"></param>
        /// <param name="oDbType"></param>
        /// <param name="oSourceColumn"></param>
        /// <param name="oSourceVersion"></param>
        public void AddInParameter(IDbCommand oCommand, string sName, DbType oDbType, string oSourceColumn, DataRowVersion oSourceVersion)
        {
            AddParameter(oCommand, sName, oDbType, 0, ParameterDirection.Input, true, 0, 0, oSourceColumn, oSourceVersion, null);
        }        

        /// <summary>
        /// Global method for adding command type for stored procedure
        /// </summary>
        /// <param name="oConnection"></param>
        /// <param name="sDbCommand"></param>
        /// <returns></returns>
        public IDbCommand GetStoredProcCommand(IDbConnection oConnection, String sDbCommand)
        {
            IDbCommand oCommand = null;

            if (GetDBType().Equals(DBORACLE))
            {   
                oCommand = new OracleCommand(sDbCommand);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Connection = (OracleConnection)oConnection;
            }

            return (OracleCommand)oCommand;
        }

        ///// <summary>
        ///// Global method for adding command type for stored procedure
        ///// </summary>
        ///// <param name="oConnection"></param>
        ///// <param name="sDbCommand"></param>
        ///// <returns></returns>
        //public IDbCommand GetStoredProcCommand(String sDbCommand)
        //{
        //    IDbCommand oCommand = null;

        //    if (GetDBType().Equals(DBORACLE))
        //    {
        //        oCommand = new OracleCommand(sDbCommand);
        //        oCommand.CommandType = CommandType.StoredProcedure;
        //        oCommand.Connection = (OracleConnection)m_oConnection;
        //    }

        //    return oCommand;
        //}

        /// <summary>
        /// Global method for adding command type for stored procedure
        /// </summary>
        /// <param name="oConnection"></param>
        /// <param name="sDbCommand"></param>
        /// <returns></returns>
        public DbCommand GetStoredProcCommand(String sDbCommand)
        {
            IDbCommand oCommand = null;

            if (GetDBType().Equals(DBORACLE))
            {
                oCommand = new OracleCommand(sDbCommand);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Connection = (OracleConnection)m_oConnection;
            }
            
            return (DbCommand)oCommand;
        }

        public IDbCommand GetStoredProcICommand(String sDbCommand)
        {
              IDbCommand oCommand = null;

            if (GetDBType().Equals(DBORACLE))
            {
                oCommand = new OracleCommand(sDbCommand);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Connection = (OracleConnection)m_oConnection;
                
            }

            return  oCommand;
        }
        public DbCommand GetFunctionCommand(String sDbCommand)
        {
            IDbCommand oCommand = null;

            if (GetDBType().Equals(DBORACLE))
            {
                oCommand = new OracleCommand(sDbCommand);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Connection = (OracleConnection)m_oConnection;
            }

            return (DbCommand)oCommand;
        }

        /// <summary>
        /// Execute the data set for the SQL string passed manually
        /// </summary>
        /// <param name="oConnection"></param>
        /// <param name="sCommandText"></param>
        /// <returns></returns>
        public DataSet ExecuteSQLDataSet(IDbConnection oConnection, string sCommandText)
        {
            DataSet oDataSet = null;
            try
            {
                if (GetDBType().Equals(DBORACLE))
                {
                    oDataSet = COracleDatabaseFactory.GetOracleDatabaseFactory().ExecuteSQLDataSet(oConnection, sCommandText);
                }
            }
            catch (Exception oEx)
            {
                throw oEx;
            }

            return oDataSet;
        }

        /// <summary>
        /// Execute the data reader for the SQL string passed manually
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <param name="sCommandText"></param>
        /// <returns></returns>
        public IDataReader ExecuteSQLDataReader(IDbCommand oDbCommand, string sCommandText)
        {
            IDataReader oDataReader = null;
            try
            {
                if (GetDBType().Equals(DBORACLE))
                {
                    oDataReader = COracleDatabaseFactory.GetOracleDatabaseFactory().ExecuteSQLDataReader(oDbCommand, sCommandText);
                }
            }
            catch (Exception oEx)
            {
                throw oEx;
            }

            return oDataReader;
        }

        /// <summary>
        /// Execute the data reader for the SQL command
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <returns></returns>
        public IDataReader ExecuteDataReader(IDbCommand oDbCommand)
        {
            IDataReader oDataReader = null;

            try
            {
                if (GetDBType().Equals(DBORACLE))
                {
                    oDataReader = COracleDatabaseFactory.GetOracleDatabaseFactory().ExecuteReader(oDbCommand);
                }
            }
            catch (Exception oEx)
            {
                throw oEx;
            }

            return oDataReader;
        }
    
        /// <summary>
        /// Execute the data set for the SQL command
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(IDbCommand oDbCommand)
        {
            DataSet oDataSet = null;

            try
            {
                if (GetDBType().Equals(DBORACLE))
                {
                    oDataSet = COracleDatabaseFactory.GetOracleDatabaseFactory().ExecuteDataSet(oDbCommand);
                }
            }
            catch (Exception oEx)
            {
                throw oEx;
            }

            return oDataSet;
        }
        
       
       
        /// <summary>
        /// Add/update data for the SQL statement passed manually
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <param name="sCommandText"></param>
        /// <returns></returns>
        public int ExecuteSQLNonQuery(IDbCommand oDbCommand, string sCommandText)
        {
            int iRetval = 0;
                
            try
            {
                if (GetDBType().Equals(DBORACLE))
                {
                    iRetval = COracleDatabaseFactory.GetOracleDatabaseFactory().ExecuteSQLNonReader(oDbCommand, sCommandText);
                }
            }
            catch (Exception oEx)
            {
                throw oEx;
            }

            return iRetval;           
        }

        /// <summary>
        /// Add/update data for the SQL command
        /// </summary>
        /// <param name="oDbCommand"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(IDbCommand oDbCommand)
        {
            int iRetval = 0;

            try
            {
                if (GetDBType().Equals(DBORACLE))
                {
                    iRetval = COracleDatabaseFactory.GetOracleDatabaseFactory().ExecuteNonReader(oDbCommand);
                }
            }
            catch (Exception oEx)
            {
                throw oEx;
            }

            return iRetval;
        }

        /// <summary>
        /// Begin the database transaction
        /// </summary>
        /// <param name="oConnection"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IDbConnection oConnection)
        {
            IDbTransaction oTransaction = null;
            try
            {
                if (GetDBType().Equals(DBORACLE))
                {
                    oTransaction = COracleDatabaseFactory.GetOracleDatabaseFactory().BeginTransaction(oConnection);
                }
            }
            catch (Exception oEx)
            {
                throw oEx;
            }
            return oTransaction;
        }

        /// <summary>
        /// Commit the transaction
        /// </summary>
        /// <param name="oTransaction"></param>
        public void CommitTransaction(IDbTransaction oTransaction)
        {
            try
            {
                if (oTransaction != null)
                {
                    oTransaction.Commit();
                    oTransaction.Dispose();
                }
            }
            catch (Exception oEx)
            {
                throw oEx;
            }
        }

        /// <summary>
        /// Rollback the transaction
        /// </summary>
        /// <param name="oTransaction"></param>
        public void Rollback(IDbTransaction oTransaction)
        {
            try
            {
                if (oTransaction != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
            }
            catch (Exception oEx)
            {
                throw oEx;
            }
        }   

        /// <summary>
        /// Close the connection
        /// </summary>
        /// <param name="oConnection"></param>
        public void CloseConnection(IDbConnection oConnection)
        {
            if (oConnection.State != ConnectionState.Closed)
            {
                try
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
                catch (Exception oEx)
                {
                    throw oEx;
                }
            }
        }

        internal static Database CreateDatabase()
        {
             return new Database();
        }

        internal void BeginTransaction()
        {
            throw new NotImplementedException();
        }
    }

}
