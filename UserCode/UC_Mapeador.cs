using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
namespace UserCode
{
    public class UC_Mapeador
    {
        public void ReaderToObject(IDataReader reader, object oBE)
        {
            PropertyInfo[] Propiedades = oBE.GetType().GetProperties();
            int N = 0;
            foreach (PropertyInfo Propiedad in Propiedades)
            {
                AttributeCollection atributos = TypeDescriptor.GetProperties(oBE)[Propiedad.Name].Attributes;
                try
                {
                    string[] Descripciones = ((DescriptionAttribute)atributos[typeof(DescriptionAttribute)]).Description.Split(',');
                    foreach (string Descripcion in Descripciones)
                        if (!string.IsNullOrEmpty(Descripcion))
                            ReaderToObject(reader, oBE, Propiedad, Descripcion, ref N);
                }
                catch (Exception)
                { }
            }

        }
        public void ReaderToObject(IDataReader reader, object oBE, PropertyInfo Propiedad, string Descripcion, ref int N)
        {
            try
            {
                N = reader.GetOrdinal(Descripcion);
                if (!(reader.GetValue(N) is System.DBNull))
                {
                    if (Propiedad.PropertyType.Name == "DateTime")
                    {
                        Propiedad.SetValue(oBE, Convert.ToDateTime(reader.GetValue(N)), null);
                    }
                    else if (Propiedad.PropertyType.Name == "Int32" || Propiedad.PropertyType.Name == "Int16" || Propiedad.PropertyType.Name == "Int64")
                    {
                        Propiedad.SetValue(oBE, Convert.ToInt32(reader.GetValue(N)), null);
                    }
                    else if (Propiedad.PropertyType.Name == "Byte[]")
                    {
                        Propiedad.SetValue(oBE, reader.GetValue(N), null);
                    }
                    else if (Propiedad.PropertyType.Name == "Decimal")
                    {
                        Propiedad.SetValue(oBE, reader.GetValue(N), null);
                    }
                    else
                    {
                        Propiedad.SetValue(oBE, reader.GetValue(N).ToString(), null);
                    }
                    N += 1;
                }
            }
            catch
            {
            }
        }
    }
}
