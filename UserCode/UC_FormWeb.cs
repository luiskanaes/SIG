using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.IO;
using BusinessEntity;



namespace UserCode
{
    public class UC_FormWeb
    {
        public void LlenarListaObjetos(ListControl lst, object[] oBE)
        {
            lst.DataSource = oBE;
            lst.DataTextField = "Nombre";
            lst.DataValueField = "Codigo";
            lst.DataBind();
        }


        public void LlenarListaObjetos(ListControl lst, object[] oBE, string Codigo, string Nombre)
        {
            lst.DataSource = oBE;
            lst.DataTextField = Nombre;
            lst.DataValueField = Codigo;
            lst.DataBind();
        }

        public void BajarArchivo(string tipoArchivo, string archivoCliente, string archivoServidor)
        {
            HttpContext.Current.Response.ContentType = "Application/" + tipoArchivo;
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + archivoCliente);
            HttpContext.Current.Response.TransmitFile(archivoServidor);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
        }

        public void LimpiarCache()
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.Subtract(new TimeSpan(1, 0, 0)));
            HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now);
            HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        }
        public static void f_llenar_grid<T>(GridView grid, List<T> list)
        {
            grid.DataSource = list.ToList();
            grid.DataBind();
        }
        public static void f_llenar_grid(GridView grid, DataTable dt)
        {
            grid.DataSource = dt;
            grid.DataBind();
        }


        public static void f_llenar_combo(DropDownList combo, List<DictionaryEntry> oList, string pAsignar = "")
        {
            try
            {
                oList.Add(new DictionaryEntry("0", "Seleccione"));
                var sortedDict = from emtry in oList
                                 orderby emtry.Key
                                 select emtry;
                combo.DataSource = sortedDict.ToList();
                combo.DataTextField = "Value";
                combo.DataValueField = "Key";
                combo.DataBind();
                if (!string.IsNullOrEmpty(pAsignar)) f_asignar_combo(combo, pAsignar);
            }
            catch
            { }
        }
        public static void f_llenar_combo(DropDownList combo, DataTable dt, String text, String value)
        {
            try
            {
                DataRow row;
                DataTable dtm;
                dtm = dt.Clone();
                row = dtm.NewRow();
                row[text] = "SELECCIONE";
                row[value] = "0";
                dtm.Rows.Add(row);
                foreach (DataRow item in dt.Rows)
                {
                    row = dtm.NewRow();
                    row[text] = item[text];
                    row[value] = item[value];
                    dtm.Rows.Add(row);
                }
                combo.DataSource = dtm;
                combo.DataTextField = text;
                combo.DataValueField = value;
                combo.DataBind();
            }
            catch (Exception ex) { throw ex; }
        }
        public static void f_llenar_combo_defecto(DropDownList combo, DataTable dt, String text, String value)
        {
            try
            {
                combo.DataSource = dt;
                combo.DataTextField = text;
                combo.DataValueField = value;
                combo.DataBind();
            }
            catch (Exception ex) { throw ex; }
        }
        public static void f_asignar_combo(DropDownList combo, String value)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i].Value.Trim() == value.Trim())
                {
                    combo.SelectedIndex = i; break;
                }
            }
        }
        public static decimal Dame_Entero_Decimal(object XobjValue)
        {
            if (object.ReferenceEquals(XobjValue, System.DBNull.Value))
            {
                return 0;
            }
            else if (XobjValue == null)
            {
                return 0;
            }
            else if (XobjValue.ToString().Trim().Equals(""))
            {
                return 0;
            }
            return Convert.ToDecimal(XobjValue);
        }

        public static void SetDefaultButton(WebControl button, TextBox tb)
        {
            if (tb.TextMode == TextBoxMode.MultiLine)
                return;
            tb.Attributes.Add(

           "onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + button.UniqueID + "').click();return false;}} else {return true}; ");
        }
        public static DateTime f_DateTime(String sFecha)
        {
            return DateTime.ParseExact(sFecha, "d/M/yyyy", null);
        }
        public static void f_CultureInfo()
        {
            CultureInfo objCI = new CultureInfo("es-ES"); //"en-US" Ingles, "es-ES" Español
            Thread.CurrentThread.CurrentCulture = objCI;
            Thread.CurrentThread.CurrentUICulture = objCI;
        }
        public static object mSQLFieldOrNull(object pvValor, tgSQLFieldType pvTipo)
        {
            object voValRet = DBNull.Value;

            if (pvValor != null)
                if (pvValor.ToString().TrimEnd().Length != 0)
                    switch (pvTipo)
                    {
                        case tgSQLFieldType.TEXT:
                            if (pvValor.ToString().Trim() != "") voValRet = pvValor.ToString().Trim();
                            break;
                        case tgSQLFieldType.TEXTDEFAULT:
                            if (pvValor.ToString().Trim() != "") voValRet = pvValor.ToString().Trim();
                            voValRet = ((string)pvValor != "" ? pvValor : DBNull.Value);
                            break;
                        case tgSQLFieldType.TEXTCODE:
                            if ((string)pvValor != "" && (string)pvValor != "0" && (string)pvValor != "00" && (string)pvValor != "000000") voValRet = pvValor.ToString().Trim();
                            break;
                        case tgSQLFieldType.NUMERIC:
                            if (Double.Parse(pvValor.ToString()) != 0) voValRet = pvValor;
                            break;
                        case tgSQLFieldType.NUMERICZERO:
                            voValRet = pvValor;
                            break;
                        case tgSQLFieldType.BOOLEAN:
                            voValRet = ((bool)pvValor ? true : false);
                            break;
                        case tgSQLFieldType.DATETIME:
                            if ((DateTime)pvValor != DateTime.MinValue) voValRet = pvValor;
                            break;
                        case tgSQLFieldType.Binary:
                            voValRet = pvValor;
                            break;
                        case tgSQLFieldType.DEFAULT:
                            voValRet = pvValor;
                            break;
                    }
            return voValRet;
        }
        public static void f_WriteToFile(string strPath, ref byte[] Buffer)
        {
            try
            {
                FileStream newFile = new FileStream(strPath, FileMode.Create);
                newFile.Write(Buffer, 0, Buffer.Length);
                newFile.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static DateTime f_DateTime_Hora(String sFecha)
        {
            return DateTime.ParseExact(sFecha, "d/M/yyyy HH:mm:ss", null);
        }

        //public static ReportDocument f_Reporte(String strRutaReporte)
        //{
        //    try
        //    {
        //        String temp = Environment.GetEnvironmentVariable("TEMP");
        //        String[] k = System.IO.Directory.GetFiles(temp);
        //        int i;

        //        for (i = 0; i <= k.Length; i++)
        //        {
        //            if (k[i].Contains(".rpt"))
        //            {
        //                System.IO.File.Delete(k[i]);
        //            }
        //        }
        //    }
        //    catch { }
        //    ReportDocument objRpt = new ReportDocument();
        //    try
        //    {
        //        objRpt.Load(strRutaReporte);
        //    }
        //    catch { }
        //    return objRpt;
        //}
    }
}
