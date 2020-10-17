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

            LogDetails logDetails = new LogDetails();
            AddressBooks addressBooksCollection = new AddressBooks();
            addressBooksCollection.Name = "General";

            bool contAddressBook = true; ;
            bool contContactPanel = true; ;

            do
            {
                Console.WriteLine("Enter\n" +
                "1 : To Add a new Address Book\n" +
                "2 : To use current address books ( " + addressBooksCollection.Name + " )\n" +
                "3 : Switch Address Book\n" +
                "0 : Exit");
                int userChoice;
                try
                {
                    userChoice = Int32.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    logDetails.LogDebug("Class : Program , Method : Main, Field : userChoice");
                    logDetails.LogError(e.Message + " It should be a integer");
                    userChoice = 0;
                }
                switch (userChoice)
                {
                    case 1:
                        contContactPanel = true;
                        Console.WriteLine("Add Name of the new Address Book");
                        addressBooksCollection.Name = Console.ReadLine();
                        break;
                    case 2:
                        contContactPanel = true;
                        break;
                    case 3:
                        contContactPanel = true;
                        Console.WriteLine("Enter Name of the Address Book you want to switch");
                        addressBooksCollection.Name = Console.ReadLine();
                        break;
                    case 0:
                        contAddressBook = false;
                        contContactPanel = false;
                        break;
                    default:
                        Console.WriteLine("Wrong Option Entered");
                        break;
                }

                int choice;

                while (contContactPanel)
                {
                    Console.WriteLine("Enter\n" +
                    "1 : Add Contact Details to " + addressBooksCollection.Name + " Address Book\n" +
                    "2 : Edit a Contact Detail\n" +
                    "3 : Delete a Contact Detail\n" +
                    "0 : Exit");
                    try
                    {
                        choice = Int32.Parse(Console.ReadLine());
                    }
                    catch(Exception e)
                    {
                        logDetails.LogDebug("Class : Program , Method : Main, Field : choice");
                        logDetails.LogError(e.Message + " It should be a integer");
                        choice = 0;
                    }
                    switch (choice)
                    {
                        case 1:
                            addressBooksCollection.AddNewContactInAddressBook();
                            break;
                        case 2:
                            addressBooksCollection.EditDetailsInAddressBook();
                            break;
                        case 3:
                            addressBooksCollection.DeleteOneContactDetail();
                            break;
                        case 0:
                            contContactPanel = false;
                            break;
                        default:
                            break;
                    }

                }



            } while (contAddressBook);


            return;
        }
    }
}
