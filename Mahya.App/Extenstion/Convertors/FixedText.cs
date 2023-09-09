using System;
using System.Collections.Generic;
using System.Text;

namespace Mahya.App.Extenstion.Convertors
{
    public class FixedText
    {
        public static string FixEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
