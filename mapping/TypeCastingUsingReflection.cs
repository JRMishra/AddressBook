using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    /*
     * This class is helpful to 
     * type cast a object dynamically during run time
     * into a specified generic class "T"
     */
    class TypeCastingUsingReflection
    {
        public static T Cast<T>(Object obj)
        {
            return (T)obj;
        }
    }
}
