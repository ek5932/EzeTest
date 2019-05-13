using System;
using System.Runtime.CompilerServices;

namespace EzeTest.Framework.Contracts
{
    public static class ContractExtensions
    {
        public static void VerifyIsSet(this object value, string propertyName)
        {

        }

        public static T VerifyIsSet<T>(this T value, string propertyName) where T : class
        {
            return value;
        }

        public static void VerifyIsSet(this Enum value, string propertyName, [CallerMemberName] string testThis = null)
        {
            
        }
    }
}
