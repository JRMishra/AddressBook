using NLog;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace AddressBook
{
    class AddressBookMain
    {
        Dictionary<string, ContactDetails> _addressBook;
        LogDetails logDetails = new LogDetails();

        public AddressBookMain()
        {
            this._addressBook = new Dictionary<string, ContactDetails>();
        }

        public AddressBookMain(Dictionary<string,ContactDetails> contactAddress)
        {
            this._addressBook = contactAddress;
        }

        public void AddContactDetails()
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

            _addressBook.Add(contact.FirstName, contact);
            Console.WriteLine("Processing...\n");
            Console.WriteLine("Details saved successfully");
            return;
        }

        public void EditContactDetails()
        {
            string name;
            Console.WriteLine("Enter First Name whose details need to be edited ");
            name = Console.ReadLine();

            if(_addressBook.ContainsKey(name))
            {

            
                bool notCompleted = true;
                int choice;

                Console.WriteLine("Enter\n" +
                        "1 : Edit City\n" +
                        "2 : Edit State\n" +
                        "3 : Edit Zip\n" +
                        "4 : Edit Phone Number\n" +
                        "5 : Edit Email ID\n" +
                        "0 : Edit Completed");

                while (notCompleted)
                {
                    try
                    {
                       choice = Int32.Parse(Console.ReadLine());
                    }
                    catch(Exception e)
                    {
                        logDetails.LogDebug("Class : AddressBookMain , Method : EditContact, Field : choice");
                        logDetails.LogError(e.Message + " It should be a integer");
                        choice = 0;
                    }
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Edit Updated City :");
                            _addressBook[name].City = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("Edit Updated State :");
                            _addressBook[name].State = Console.ReadLine();
                            break;
                        case 3:
                            Console.Write("Edit Updated Zip :");
                            _addressBook[name].Zip = Console.ReadLine();
                            break;
                        case 4:
                            Console.Write("Edit Updated Phone Number :");
                            _addressBook[name].PhoneNumber = Console.ReadLine();
                            break;
                        case 5:
                            Console.Write("Edit Updated Email Id :");
                            _addressBook[name].State = Console.ReadLine();
                            break;
                        case 0:
                            notCompleted = false;
                            break;
                        default:
                            Console.WriteLine("Wrong Choice\nChoose Again");
                            break;
                    }
                    if (choice != 0)
                        Console.WriteLine("\nIf there is anything else to edit, enter respective number\n" + "else enter 0 to exit");
                }
                
            }
            else
            {
                Console.WriteLine("Details of " + name + " is not present");
            }
        }

        public void DeleteContactDetails()
        {
            string name;
            Console.WriteLine("Enter First Name whose details need to be deleted ");
            name = Console.ReadLine();

            if (_addressBook.ContainsKey(name))
            {
                _addressBook.Remove(name);
                Console.WriteLine("Details of " + name + " deleted successfully");
            }  
            else
                Console.WriteLine("Details of " + name + " is not present");
            return;
        }
        
        public void DisplayAllContacts()
        {
            Console.WriteLine("All Contacts are :");
            foreach(var item in _addressBook)
            {
                Console.WriteLine(item.Value.Display());
            }
        }
    }
}
