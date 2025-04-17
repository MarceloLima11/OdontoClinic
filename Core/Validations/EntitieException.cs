using System;

namespace Core.Validations
{
    public class EntitieException : Exception
    {
        public EntitieException(string error) : base(error)
        { }

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new EntitieException(error);
        }
    }
}
