using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;


/// <summary>
/// Descripción breve de GridViewExporUtil
/// </summary>

public class GridViewExportUtil
{

    public static void Export(string fileName, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        //  Create a form to contain the grid
        Table table = new Table();
        table.GridLines = gv.GridLines;
        //  add the header row to the table
        if ((((gv.HeaderRow) != null)))
        {
            GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
            table.Rows.Add(gv.HeaderRow);
        }
        //  add each of the data rows to the table
        foreach (GridViewRow row in gv.Rows)
        {
            GridViewExportUtil.PrepareControlForExport(row);
            table.Rows.Add(row);
        }
        //  add the footer row to the table
        if ((((gv.FooterRow) != null)))
        {
            GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
            table.Rows.Add(gv.FooterRow);
        }
        //  render the table into the htmlwriter
        table.RenderControl(htw);
        //  render the htmlwriter into the response
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }
   
    public static void ExportPlus(string fileName, GridView gv, string sAdicional)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        //  Create a form to contain the grid
        Table table = new Table();
        table.GridLines = gv.GridLines;
        //  add the header row to the table
        if ((((gv.HeaderRow) != null)))
        {
            GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
            table.Rows.Add(gv.HeaderRow);
        }
        //  add each of the data rows to the table
        foreach (GridViewRow row in gv.Rows)
        {
            GridViewExportUtil.PrepareControlForExport(row);
            table.Rows.Add(row);
        }
        //  add the footer row to the table
        if ((((gv.FooterRow) != null)))
        {
            GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
            table.Rows.Add(gv.FooterRow);
        }
        //  render the table into the htmlwriter
        table.RenderControl(htw);
        //  render the htmlwriter into the response

        String strTextHtml = sAdicional + sw.ToString();

        //HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.Write(strTextHtml);
        HttpContext.Current.Response.End();
    }

    // Replace any of the contained controls with literals
    private static void PrepareControlForExport(Control control)
    {
        int i = 0;
        while ((i < control.Controls.Count))
        {
            Control current = control.Controls[i];
            if ((current is LinkButton))
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl(((LinkButton)current).Text));
            }
            else if ((current is ImageButton))
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl(((ImageButton)current).AlternateText));
            }
            else if ((current is HyperLink))
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl(((HyperLink)current).Text));
            }
            else if ((current is DropDownList))
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl(((DropDownList)current).SelectedItem.Text));
            }
            else if ((current is CheckBox))
            {
                control.Controls.Remove(current);
                //control.Controls.AddAt(i, new LiteralControl(((CheckBox)current).Checked));
                //TODO: Warning!!!, inline IF is not supported ?
            }
            if (current.HasControls())
            {
                GridViewExportUtil.PrepareControlForExport(current);
            }
            i = (i + 1);
        }
    }
}

