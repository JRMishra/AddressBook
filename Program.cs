using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace AddressBook
{
    class AddressBookMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book");
            Console.WriteLine("========================");

            Dictionary<string, ContactDetails> addressBook = new Dictionary<string, ContactDetails>();

            Console.WriteLine("Enter\n" +
                "1 : Add Contact Details To Address Book\n" +
                "2 : Edit a Contact Detail\n" +
                "3 : Delete a Contact Detail\n" +
                "4 : Exit");
            int choice = Int32.Parse(Console.ReadLine());

            switch(choice)
            {
                case 1:
                    AddContactDetails(ref addressBook);
                    break;
                case 2:
                case 3:
                case 4:
                default:
                    break;
            }

            return;
        }

        static void AddContactDetails(ref Dictionary<string,ContactDetails> addressBook)
        {
            ContactDetails contact = new ContactDetails();

            Console.WriteLine("Enter\n");

            Console.Write("First Name : ");
            contact.FirstName = Console.ReadLine();
            Console.Write("Last Name : ");
            contact.LastName = Console.ReadLine();
            Console.Write("City : ");
            contact.City = Console.ReadLine();
            Console.Write("State : ");
            contact.State = Console.ReadLine();
            Console.Write("Zip : ");
            contact.Zip = Console.ReadLine();
            Console.Write("Phone Number : ");
            contact.PhoneNumber = Console.ReadLine();
            Console.Write("Email : ");
            contact.Email = Console.ReadLine();

            addressBook.Add(contact.FirstName, contact);

            return;
        }
    }
}
