using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book");
            Console.WriteLine("========================");

            AddressBookMain addressBook = new AddressBookMain();

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
                        int numDetails;
                        Console.Write("Enter number of Contact Details you want to save : ");
                        numDetails = Int32.Parse(Console.ReadLine());
                        for(int i=1; i<=numDetails; i++)
                            addressBook.AddContactDetails();
                        break;
                    case 2:
                        addressBook.EditContactDetails();
                        break;
                    case 3:
                        addressBook.DeleteContactDetails();
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        break;
                }
            }

            addressBook.DisplayAllContacts();

            return;
        }
    }
}
