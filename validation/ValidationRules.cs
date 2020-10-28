using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.validation
{
    class ValidationRules
    {
        public static string FirstNameRule { get; } = "1. min 3 characters\n2. first letter in upper case";
        public static string LastNameRule { get; } = "1. min 3 characters\n2. first letter in upper case";
        public static string CityRule { get; } = "1. min 3 characters\n2. first letter in upper case";
        public static string StateRule { get; } = "1. min 3 characters\n2. first letter in upper case";
        public static string ZipRule { get; } = "1. 6 digit number";
        public static string PhoneNumberRule { get; } = "1. 2 digit country code(optional) followed by space\n2. 10 digit number";
        public static string EmailRule { get; } = "1. \" abc.xyz@bl.co.in \" format\n (.xyz & .in parts optional)";
    }
}
