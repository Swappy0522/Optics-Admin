using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;

/// <summary>
/// Summary description for MyExtensionMethod
/// </summary>
public static class MyExtensionMethod
{
    public static string SwapitWith(this string KeyString, string Delimeter)
    {
        if (KeyString != null && KeyString != "")
        {
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            KeyString = r.Replace(KeyString, " ");
            string[] SplitArray = KeyString.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            KeyString = String.Join(Delimeter, SplitArray);
        }

        return KeyString;
    }

    public static string SwapItWithDash(string text)
    {
        string newText = "";

        try
        {
            newText = Regex.Replace(text, "[^a-zA-Z0-9]", " ");

            string[] SplitArray = newText.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            newText = String.Join("-", SplitArray);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return newText;
    }

    public static string SwapItWithUnderscore(string text)
    {
        string newText = "";

        try
        {
            newText = Regex.Replace(text, "[^a-zA-Z0-9]", "_");
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return newText;
    }

    public static string GetCurrencyFormat(this string Price)
    {
        CultureInfo CInfo = new CultureInfo("hi-IN");
        if (Price != "")
        {
            Decimal Amount = Math.Round(Convert.ToDecimal(Price));
            return Amount.ToString("N", CInfo).Replace(".00", "");
        }
        else
        {
            string NullValue = "00";
            return NullValue;
        }

    }

    public static string GetCompleteRawURL(this System.Web.HttpRequest Request)
    {
        //Method 1
        string _completeRawURL = Request.Url.GetLeftPart(UriPartial.Authority) + Request.RawUrl.ToString();
        // OR
        //Method 2
        string strPathAndQuery = Request.Url.PathAndQuery;
        string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
        strUrl = strUrl.Remove(strUrl.Length - 1, 1);
        string CompleteRawURL = strUrl + Request.RawUrl.ToString();

        if (_completeRawURL == CompleteRawURL)
        {
            return _completeRawURL;
        }
        else
        {
            return CompleteRawURL;
        }


    }

    public static void CallJavascriptScriptFunction(this Page _page, string JavascriptFunctionName)
    {
        ScriptManager.RegisterStartupScript(_page, _page.GetType(), "CallScript", JavascriptFunctionName, true);
    }

    #region Call MasterPage OR ContentPage Method

    public static void InvokeMethod(this Page _page, string MethodName)
    {
        Type contentType = _page.GetType();
        System.Reflection.MethodInfo mi = contentType.GetMethod("FillCustomerReview");
        if (mi != null)
        {
            mi.Invoke(_page, new object[] { });
        }
    }

    public static void InvokeMethod(this Page _page, string MethodName, Object[] parameters)
    {
        Type contentType = _page.GetType();
        System.Reflection.MethodInfo mi = contentType.GetMethod(MethodName);
        if (mi != null)
        {
            mi.Invoke(_page, parameters);
        }
    }

    public static void InvokeMethod(this MasterPage _masterPage, string MethodName)
    {
        Type contentType = _masterPage.GetType();
        System.Reflection.MethodInfo mi = contentType.GetMethod(MethodName);
        if (mi != null)
        {
            mi.Invoke(_masterPage, new object[] { });
        }
    }

    public static void InvokeMethod(this MasterPage _masterPage, string MethodName, Object[] parameters)
    {
        Type contentType = _masterPage.GetType();
        System.Reflection.MethodInfo mi = contentType.GetMethod(MethodName);
        if (mi != null)
        {
            mi.Invoke(_masterPage, parameters);
        }
    }

    #endregion

    public static bool IsContainsFalse(this DataTable dt, string ColumnName)
    {
        bool result = false;
        for (int i = 1; i < dt.Columns.Count; i++)
        {
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                if (dt.Rows[row][ColumnName].ToString() == "False")
                {
                    result = true;
                    break;
                }
            }
            if (result)
            {
                break;
            }
        }

        return result;
    }

    public static string Sanatize(this string Text)
    {
        Text = Text.Replace("&lt;", "<");
        Text = Text.Replace("&gt;", ">");
        Text = Text.Replace("'", "''");
        string regEx = "<.*?>.*?</.*?>";
        string tagless = Regex.Replace(Text, regEx, string.Empty);
        tagless = Regex.Replace(tagless, @"\<[^\<\>]*\>", String.Empty);
        tagless = tagless.Replace("<", string.Empty).Replace(">", string.Empty);
        Text = tagless.Trim();
        //if (Text == "" || Text == null) throw new Exception("string can not be empty after applying ToClear() to it.");
        return Text;
    }

    public static DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
    {
        DataTable dt = new DataTable();
        PropertyInfo[] columns = null;
        if (Linqlist == null) return dt;

        foreach (T Record in Linqlist)
        {
            if (columns == null)
            {
                columns = ((Type)Record.GetType()).GetProperties();
                foreach (PropertyInfo GetProperty in columns)
                {
                    Type colType = GetProperty.PropertyType;

                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                    == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }

                    dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                }
            }

            DataRow dr = dt.NewRow();

            foreach (PropertyInfo pinfo in columns)
            {
                dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                (Record, null);
            }

            dt.Rows.Add(dr);
        }
        return dt;
    }
    public static string Sanatize(this string Text, bool IsNullable)
    {
        Text = Text.Replace("&lt;", "<");
        Text = Text.Replace("&gt;", ">");
        Text = Text.Replace("'", "''");
        string regEx = "<.*?>.*?</.*?>";
        string tagless = Regex.Replace(Text, regEx, string.Empty);
        tagless = Regex.Replace(tagless, @"\<[^\<\>]*\>", String.Empty);
        tagless = tagless.Replace("<", string.Empty).Replace(">", string.Empty);
        Text = tagless.Trim();
        return Text;
    }

    public static object ToDbNull(this object Value)
    {
        if (Value != null && Value.ToString() != "")
        {
            return Value;
        }
        else
        {
            return DBNull.Value;
        }
    }

    public static string retWord(int number)
    {

        if (number == 0) return "Zero";

        if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";

        int[] num = new int[4];

        int first = 0;

        int u, h, t;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (number < 0)
        {

            sb.Append("Minus");

            number = -number;

        }

        string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };

        string[] words = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };

        string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };

        string[] words3 = { "Thousand ", "Lakh ", "Crore " };

        num[0] = number % 1000; // units

        num[1] = number / 1000;

        num[2] = number / 100000;

        num[1] -= 100 * num[2]; // thousands

        num[3] = number / 10000000; // crores

        num[2] -= 100 * num[3]; // lakhs



        for (int i = 3; i > 0; i--)
        {

            if (num[i] != 0)
            {

                first = i;

                break;

            }

        }

        for (int i = first; i >= 0; i--)
        {

            if (num[i] == 0) continue;

            u = num[i] % 10; // ones

            t = num[i] / 10;

            h = num[i] / 100; // hundreds

            t -= 10 * h; // tens

            if (h > 0) sb.Append(words0[h] + "Hundred ");

            if (u > 0 || t > 0)
            {

                if (h > 0 || i == 0) sb.Append("and ");

                if (t == 0)

                    sb.Append(words0[u]);

                else if (t == 1)

                    sb.Append(words[u]);

                else

                    sb.Append(words2[t - 2] + words0[u]);

            }

            if (i != 0) sb.Append(words3[i - 1]);

        }

        return sb.ToString().TrimEnd() + " Only";

    }

   
    public static string DecryptString(string EString)
    {
        try
        {
            EString = Crypts.Decrypt(EString);
        }
        catch { }
        return EString;
    }

    public static string EncryptString(string EString)
    {
        if (EString.Trim() != "")
        {
            try
            {
                EString = Crypts.Encrypt(EString);
            }
            catch { }
        }
        return EString;
    }

    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

   
}






