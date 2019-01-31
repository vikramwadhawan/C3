using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Colligo.O365Product.Helper.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// This function extracts a string from a sentence lies between two strings
        /// </summary>
        /// <param name="text">Sentence from which string will be extracted</param>
        /// <param name="firstString">
        /// Starting string after which the result string will be extracted
        /// </param>
        /// <param name="lastString">
        /// Ending string before which the result string will be extracted
        /// </param>
        /// <returns>Final string extracted from the Text parameter</returns>
        public static string Between(this string text, string firstString, string lastString)
        {
            string finalString = string.Empty;
            if (string.IsNullOrEmpty(text))
                return finalString;
            string str = text;
            int startIndex = str.IndexOf(firstString);
            int endPos = str.IndexOf(lastString);
            if (startIndex == -1 || endPos == -1)
                return finalString;
            int startPos = startIndex + firstString.Length;
            if (startPos > endPos)
                return finalString;
            finalString = str.Substring(startPos, endPos - startPos);
            return finalString;
        }

        public static string ReplaceString(this string s)
        {
            StringBuilder sb = new StringBuilder(s);
            bool isWhereExist = s.Contains("<Where>");
            if (isWhereExist)
            {
                sb.Replace("<Where>", "<Where><And>");
                sb.Replace("</Where>", "<Eq><FieldRef Name='FSObjType'/><Value Type='Integer'>1</Value></Eq></And></Where>");
            }
            else
            {
                sb.Append("<Where><Eq><FieldRef Name='FSObjType'/><Value Type='Integer'>1</Value></Eq></Where>");
            }
            return sb.ToString();
        }

        public static bool HasValue(this string stringValue)
        {
            return !string.IsNullOrEmpty(stringValue);
        }

        public static bool IsNull<T>(this T entity)
        {
            return (entity == null);
        }

        public static bool IsNotNull<T>(this T entity)
        {
            return (entity != null);
        }

        public static bool HasItems<T>(this IList<T> entity)
        {
            return (entity != null && entity.Count > 0);
        }

        public static string ToEncodedString(this string stringValue)
        {
            if (IsNull(stringValue))
                return "";
            return HttpUtility.UrlEncode(stringValue);
        }

        public static string ToDecodedString(this string stringValue)
        {
            if (IsNull(stringValue))
                return "";
            return HttpUtility.UrlDecode(stringValue);
        }

        public static string RemoveSpecialCharacters(this string stringValue)
        {
            stringValue = stringValue.ToDecodedString();
            StringBuilder sb = new StringBuilder();
            foreach (char c in stringValue)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z')
                    || (c >= 'a' && c <= 'z') || c == '_' || c == '-')
                {
                    sb.Append(c);
                } else if ((c == '!' || c == '@' || c == '$' || c == '^' || c == '&' || c == '(' || c == ')'))
                         {

                    sb.Append(c);
                }
                    

                    else if ((c == ',' || c == '.' || c == ';' || c == '[' || c == ']' || c == '`' || c == '~'))
                {

                    sb.Append(c);
                }
                else
                {
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }

        public static byte[] ReadFully(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
