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
            bool exit = false;
            int choice;
            while (!exit)
            {
                Console.WriteLine("Enter\n" +
                "1 : Add Contact Details To Address Book\n" +
                "2 : Edit a Contact Detail\n" +
                "3 : Delete a Contact Detail\n" +
                "4 : Exit");
            choice = Int32.Parse(Console.ReadLine());
            
                switch (choice)
                {
                    case 1:
                        AddContactDetails(ref addressBook);
                        break;
                    case 2:
                        EditContactDetails(ref addressBook);
                        break;
                    case 3:
                        DeleteContactDetails(ref addressBook);
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        break;
                }
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
            Console.WriteLine();

            addressBook.Add(contact.FirstName, contact);

            return;
        }

        static void EditContactDetails(ref Dictionary<string, ContactDetails> addressBook)
        {
            string name;
            Console.WriteLine("Enter First Name whose details need to be edited ");
            name = Console.ReadLine();

            bool notCompleted = true;
            int choice;

            Console.WriteLine("Enter\n" +
                    "1 : Edit City\n" +
                    "2 : Edit State\n+" +
                    "3 : Edit Zip\n" +
                    "4 : Edit Phone Number\n" +
                    "5 : Edit Email ID\n" +
                    "0 : Edit Completed");

            while (notCompleted)
            {
                choice = Int32.Parse(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        Console.Write("Edit Updated City :");
                        addressBook[name].City = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Edit Updated State :");
                        addressBook[name].State = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Edit Updated Zip :");
                        addressBook[name].Zip = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Edit Updated Phone Number :");
                        addressBook[name].PhoneNumber = Console.ReadLine();
                        break;
                    case 5:
                        Console.Write("Edit Updated Email Id :");
                        addressBook[name].State = Console.ReadLine();
                        break;
                    case 0:
                        notCompleted = false;
                        break;
                    default:
                        Console.WriteLine("Wrong Choice\nChoose Again");
                        break;
                }

                Console.WriteLine("If there is anything else to edit, enter respective number\n" +
                    "else enter 0 to exit");
            }
        }

        static void DeleteContactDetails(ref Dictionary<string, ContactDetails> addressBook)
        {
            string name;
            Console.WriteLine("Enter First Name whose details need to be deleted ");
            name = Console.ReadLine();

            if (addressBook.ContainsKey(name))
            {
                addressBook.Remove(name);
                Console.WriteLine("Details of " + name + " deleted successfully");
            }  
            else
                Console.WriteLine("Details of " + name + " is not present");
            return;
        }
    }
}
