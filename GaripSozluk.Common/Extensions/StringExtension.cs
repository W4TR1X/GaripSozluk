using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.Extensions
{
    public static class StringExtension
    {
        public static string AsIdCode(this string value)
        {
            if (value.Length == 0)
            {
                throw new ArgumentNullException();
            }

            value = value.ToLower()
                //Replacing
                .Replace(" ", "-")
                .Replace("ç", "c")
                .Replace("ş", "s")
                .Replace("ü", "u")
                .Replace("ğ", "g")
                .Replace("ı", "i")
                .Replace("ö", "o")
                //Removing
                .Replace("'", "")
                .Replace("\"", "")
                .Replace("\\", "");

            if (value.Length > 45)
            {
                value = value.Substring(0, 45);
            }

            value += "-" + new Random().Next(1000, 9999);

            return value;
        }
    }
}
