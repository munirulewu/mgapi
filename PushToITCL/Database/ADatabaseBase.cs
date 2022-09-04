/*
 * File name            : ADatabaseBase.cs
 * Author               : Munirul Islam
 * Date                 : March 24, 2014
 * Version              : 1.0
 *
 * Description          : The base class used for creating database connection 
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


namespace PushDataMGoITCL
{
    /// <summary>
    /// Declares abstract method for database factory
    /// </summary>
    public abstract class ADatabaseBase
    {

        #region Abstract Functions

        //Connection/transaction specific
        protected abstract IDbConnection GetConnection(string sConnName);
        public abstract IDbTransaction BeginTransaction(IDbConnection oConnection);        
        public abstract IDbConnection OpenConnection(string sConnName);
        public abstract void CloseConnection(IDbConnection oDBConnection);

        //Database operation specific
        public abstract IDataReader ExecuteSQLDataReader(IDbCommand oCommand, string commandText);
        public abstract IDataReader ExecuteReader(IDbCommand oCommand);
        public abstract DataSet ExecuteSQLDataSet(IDbConnection oConnection, string commandText);
        public abstract DataSet ExecuteDataSet(IDbCommand oDbCommand);
        public abstract int ExecuteSQLNonReader(IDbCommand oCommand, string commandText);
        public abstract int ExecuteNonReader(IDbCommand oDbCommand);
 
        #endregion
    }
}
