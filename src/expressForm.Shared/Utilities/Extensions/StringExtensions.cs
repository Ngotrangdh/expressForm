using System;
using System.Collections.Generic;
using System.Text;

namespace expressForm.Shared.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string ToStringOrEmpty(this string value)
        {
            return value ?? string.Empty;
        }
    }
}
