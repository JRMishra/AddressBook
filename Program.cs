using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
                "4 : Search across all address books\n" +
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
                    case 4:
                        Console.WriteLine("Enter\n" +
                            "1 : Search by State\n" +
                            "2 : Search by City");
                        switch(Int32.Parse(Console.ReadLine()))
                        {
                            case 1:
                                Console.Write("Enter state name : ");
                                string state = Console.ReadLine();
                                addressBooksCollection.SearchAllAddressBooksByState(state);
                                break;
                            case 2:
                                Console.Write("Enter city name : ");
                                string city = Console.ReadLine();
                                addressBooksCollection.SearchAllAddressBooksByCity(city);
                                break;
                            default:
                                Console.WriteLine("Wrong Choice\n");
                                break;
                        }
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
                    "4 : Search across a state\n" +
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
                        case 4:
                            Console.Write("Enter State Name to search : ");
                            string stateToSearch = Console.ReadLine();
                            addressBooksCollection.SearchCurrentAddressBookByState(stateToSearch);
                            break;
                        case 0:
                            contContactPanel = false;
                            break;
                        default:
                            break;
                    }

                }

            } while (contAddressBook);

            addressBooksCollection.DisplayPersonNameByState();
            addressBooksCollection.DisplayPersonNameByCity();

            return;
        }
    }
}
