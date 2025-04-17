using Core.Enums;
using System;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsSexoValid(this string sexo)
        {
            if (string.IsNullOrWhiteSpace(sexo))
                return false;

            string normalizado = sexo.Trim().ToLower();

            return Enum.IsDefined(typeof(ESexo), normalizado);
        }
    }
}
