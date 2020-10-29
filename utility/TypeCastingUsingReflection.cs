using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    /// <summary>
     ///This class is helpful to 
     ///type cast a object dynamically during run time
     ///into a specified generic class "T"
     /// </summary>
    class TypeCastingUsingReflection
    {
        /// <summary>
        /// To type cast dynamically during run time
        /// </summary>
        /// <typeparam name="T">Class to type cast as</typeparam>
        /// <param name="obj">Object to type cast</param>
        /// <returns></returns>
        public static T Cast<T>(Object obj)
        {
            return (T)obj;
        }
    }
}
