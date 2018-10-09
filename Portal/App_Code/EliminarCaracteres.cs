using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for EliminarCaracteres
/// </summary>
public class EliminarCaracteres
{
    public EliminarCaracteres()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public static string RemoveAccentsWithNormalization(string inputString)
    {
        string normalizedString = inputString.Normalize(NormalizationForm.FormD);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < normalizedString.Length; i++)
        {
            UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(normalizedString[i]);
            }
        }
        return (sb.ToString().Normalize(NormalizationForm.FormC));
    }
    public static string ReemplazarCaracteresEspeciales(string origen)
    {
        string destino = "";
        origen = RemoveAccentsWithNormalization(origen);
        List<string> listCaracteres = new List<string>();
        for (int i = 0; i < origen.Length; i++)
        {
            listCaracteres.Add(origen[i].ToString());
        }

        for (int i = 0; i < listCaracteres.Count; i++)
        {
            for (int j = 0; j < caracteres.Length; j = j + 2)
            {
                if (listCaracteres[i] == caracteres[j])
                {
                    listCaracteres[i] = listCaracteres[i].Replace(listCaracteres[i], caracteres[j + 1]);
                    j = caracteres.Length + 1;
                }
            }
        }

        for (int i = 0; i < listCaracteres.Count; i++)
        {
            destino = destino + listCaracteres[i];
        }

        return destino;
    }
    private static string[] caracteres =
   {
                "#", "",
                "$", "",
                "&", "",
                "?", "",
                "¡", "",
                "!", "",
                "°", "",
                "Á", "A",
                "É", "E",
                "Í", "I",
                "Ó", "O",
                "Ú", "U",
                "á", "a",
                "é", "e",
                "í", "i",
                "ó", "o",
                "ú", "u",
                " ","_",
                "A?","A",
                "E?","E",
                "I?","I",
                "O?","O",
                "U?","U",
                ",","",
                ";","",
                "\u0093","",

    };
}