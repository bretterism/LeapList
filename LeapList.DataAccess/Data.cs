﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LeapList.DataAccess
{

    public class DataAccess : IDisposable
    {
        //http://stackoverflow.com/questions/10091764/c-sharp-stored-procedures
        // This class is great, but I want to create my own eventually.

        #region declarations

        private SqlCommand _cmd;
        private string SqlConnString = System.Configuration.ConfigurationManager.
            ConnectionStrings["LeapList_DBConnectionString"].ConnectionString;

        #endregion

        #region constructors

        public DataAccess()
        {
            _cmd = new SqlCommand();
            _cmd.CommandTimeout = 240;
        }

        #endregion

        #region IDisposable implementation

        ~DataAccess()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cmd.Connection.Dispose();
                _cmd.Dispose();
            }
        }

        #endregion

        #region data retrieval methods

        public DataTable ExecReturnDataTable()
        {
            using (var conn = new SqlConnection(SqlConnString))
            {
                try
                {
                    PrepareCommandForExecution(conn);
                    using (SqlDataAdapter adap = new SqlDataAdapter(_cmd))
                    {
                        DataTable dt = new DataTable();
                        adap.Fill(dt);
                        return dt;
                    }
                }
                finally
                {
                    _cmd.Connection.Close();
                }
            }
        }

        public object ExecScalar()
        {
            using (var conn = new SqlConnection(SqlConnString))
            {
                try
                {
                    PrepareCommandForExecution(conn);
                    return _cmd.ExecuteScalar();
                }
                finally
                {
                    _cmd.Connection.Close();
                }
            }
        }

        #endregion

        #region data insert and update methods

        public void ExecNonQuery()
        {
            using (var conn = new SqlConnection(SqlConnString))
            {
                try
                {
                    PrepareCommandForExecution(conn);
                    _cmd.ExecuteNonQuery();
                }
                finally
                {
                    _cmd.Connection.Close();
                }
            }
        }

        #endregion

        #region helper methods

        public void AddParm(string ParameterName, SqlDbType ParameterType, object Value)
        { _cmd.Parameters.Add(ParameterName, ParameterType).Value = Value; }

        private SqlCommand PrepareCommandForExecution(SqlConnection conn)
        {
            try
            {
                _cmd.Connection = conn;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandTimeout = this.CommandTimeout;
                _cmd.Connection.Open();

                return _cmd;
            }
            finally
            {
                _cmd.Connection.Close();
            }
        }

        #endregion

        #region properties

        public int CommandTimeout
        {
            get { return _cmd.CommandTimeout; }
            set { _cmd.CommandTimeout = value; }
        }

        public string ProcedureName
        {
            get { return _cmd.CommandText; }
            set { _cmd.CommandText = value; }
        }

        #endregion
    }
}