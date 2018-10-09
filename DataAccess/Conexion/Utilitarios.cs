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
    public class Utilitarios
    {
        Database BD = DatabaseFactory.CreateDatabase(ConfigurationManager.AppSettings.Get("CadenaConexion"));
        public DataSet EjecutaDataSet(string Procedure, params object[] Parametros)
        {
            try
            {
                return BD.ExecuteDataSet(Procedure, Parametros);
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

        public DataTable EjecutaDatatable(string Procedure, params object[] Parametros)
        {
            DataTable dtResultado = null;
            try
            {
                dtResultado = new DataTable();
                dtResultado.Load(BD.ExecuteReader(Procedure, Parametros));
                return dtResultado;
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

        public IDataReader EjecutaDataReader(string Procedure, params object[] Parametros)
        {
            try
            {
                return BD.ExecuteReader(Procedure, Parametros);
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
            try
            {
                return BD.ExecuteNonQuery(Procedure, Parametros);
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
        public object ExecuteScalar(string Procedure, params object[] Parametros)
        {
            try
            {
                return BD.ExecuteScalar(Procedure, Parametros);
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
    }
}
