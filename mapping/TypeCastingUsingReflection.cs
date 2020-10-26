using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    class TypeCastingUsingReflection
    {
        public static T Cast<T>(Object obj)
        {
            return (T)obj;
        }
    }
}
