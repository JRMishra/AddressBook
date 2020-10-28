using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AddressBook.validation
{
    class ValidateContact
    {
        public static string BookNamePattern { get; } = "^[A - Z][a - z A - Z]{2,}$";
        public static string FirstNamePattern { get; } = "^[A-Z][a-z A-Z]{2,}$";
        public static string LastNamePattern { get; } = "^[A-Z][a-z A-Z]{2,}$";
        public static string CityPattern { get; } = "^[A-Z][a-z A-Z]{2,}$";
        public static string StatePattern { get; } = "^[A-Z][a-z A-Z]{2,}$";
        public static string ZipPattern { get; } = "^[1-9][0-9]{5}$";
        public static string PhoneNumberPattern { get; } = "^([0-9][ ])?[1-9][0-9]{9}$";
        public static string EmailPattern { get; } = "^[a-z0-9A-Z]+([-.+_][a-z0-9+-]+)*@[a-z0-9A-Z]+[.][a-z]{2,3}([.][a-z]{2,})?$";

        public static bool[] ValidateContactDetails(ContactDetails contact)
        {
            bool[] validationResult = new bool[7];
            validationResult[0] = Regex.IsMatch(contact.FirstName, FirstNamePattern);
            validationResult[0] = Regex.IsMatch(contact.LastName, LastNamePattern);
            validationResult[0] = Regex.IsMatch(contact.City, CityPattern);
            validationResult[0] = Regex.IsMatch(contact.State, StatePattern);
            validationResult[0] = Regex.IsMatch(contact.Zip, ZipPattern);
            validationResult[0] = Regex.IsMatch(contact.PhoneNumber, PhoneNumberPattern);
            validationResult[0] = Regex.IsMatch(contact.Email, EmailPattern);

            return validationResult;
        }
    }
}
