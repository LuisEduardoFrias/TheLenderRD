
using System;

namespace TheLenderRD.Persistence.EntensionMethods
{
    public static class ExtensionMethodObject
    {
        public static int ToInt(this object obj) =>
        Convert.ToInt32(obj);

        public static decimal ToDecimal(this object obj) =>
        Convert.ToDecimal(obj);
    }
}
