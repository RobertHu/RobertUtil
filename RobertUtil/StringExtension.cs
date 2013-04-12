using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtil
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }
    }
}
