using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess.Conexion
{
    public class Util
    {
        Database BD = DatabaseFactory.CreateDatabase(ConfigurationManager.AppSettings.Get("CadenaConexion"));

        public DataSet EjecutaDataSet(string Procedure, params object[] Parametros)
        {
            DbConnection ocn = BD.CreateConnection();
            DbCommand dbcmd = BD.GetStoredProcCommand(Procedure, Parametros);
            dbcmd.CommandTimeout = 99999;
            dbcmd.Connection = ocn;
            try
            {
                DataSet ds = new DataSet();
                ocn.Open();
                DbDataAdapter dbdap = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                dbdap.SelectCommand = dbcmd;
                dbdap.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ocn.Close();
            }
        }

        public DataTable EjecutaDatatable(string Procedure, params object[] Parametros)
        {
            DbConnection ocn = BD.CreateConnection();
            DbCommand dbcmd = BD.GetStoredProcCommand(Procedure, Parametros);
            dbcmd.CommandTimeout = 99999;
            dbcmd.Connection = ocn;
            try
            {
                DataTable dt = new DataTable();
                ocn.Open();
                DbDataAdapter dbdap = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                dbdap.SelectCommand = dbcmd;
                dbdap.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ocn.Close();
            }
        }

        public IDataReader EjecutaDataReader(string Procedure, params object[] Parametros)
        {
            DbCommand dbcmd = BD.GetStoredProcCommand(Procedure, Parametros);
            dbcmd.CommandTimeout = 99999;
            try
            {
                return BD.ExecuteReader(dbcmd);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EjecutaQuery(string Procedure, params object[] Parametros)
        {
            DbCommand dbcmd = BD.GetStoredProcCommand(Procedure, Parametros);
            dbcmd.CommandTimeout = 99999;
            try
            {
                return BD.ExecuteNonQuery(dbcmd);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public object ExecuteScalar(string Procedure, params object[] Parametros)
        {
            DbCommand dbcmd = BD.GetStoredProcCommand(Procedure, Parametros);
            dbcmd.CommandTimeout = 99999;
            try
            {
                return BD.ExecuteScalar(dbcmd);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
