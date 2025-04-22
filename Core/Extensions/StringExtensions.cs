using System;
using Core.Enums;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsSexoValid(this string sexo)
        {
            if (string.IsNullOrWhiteSpace(sexo))
                return false;

            return Enum.TryParse<ESexo>(sexo.Trim(), ignoreCase: true, out _);
        }
    }
}
