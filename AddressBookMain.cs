using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace AddressBook
{
    class AddressBookMain
    {
        Dictionary<string, ContactDetails> _addressBook;
        Dictionary<string, List<string>> _personByState = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> _personByCity = new Dictionary<string, List<string>>();
        
        LogDetails logDetails = new LogDetails();

        public AddressBookMain()
        {
            this._addressBook = new Dictionary<string, ContactDetails>();
        }

        public AddressBookMain(Dictionary<string,ContactDetails> contactAddress)
        {
            this._addressBook = contactAddress;
        }

           //===================================================================//
          //------------------------[ Public Methods ]-------------------------//
         //--------------------------CRUD Operations--------------------------//
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        public void AddContactDetails()
        {
            ContactDetails contact = new ContactDetails();

            Console.WriteLine("Enter\n");

            Console.Write("First Name : ");
            contact.FirstName = Console.ReadLine();
            if(_addressBook.ContainsKey(contact.FirstName))
            {
                Console.WriteLine("You can't enter duplicate contact details in the same address book");
                return;
            }   

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
            AddToStateDict(contact.State, contact.FirstName);
            AddToCityDict(contact.City, contact.FirstName);

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
                            DeleteFromCityDict(_addressBook[name].City, name);
                            _addressBook[name].City = Console.ReadLine();
                            AddToCityDict(_addressBook[name].City, name);
                            break;
                        case 2:
                            Console.Write("Edit Updated State :");
                            DeleteFromStateDict(_addressBook[name].State, name);
                            _addressBook[name].State = Console.ReadLine();
                            AddToStateDict(_addressBook[name].State, name);
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
                DeleteFromStateDict(_addressBook[name].State, name);
                DeleteFromCityDict(_addressBook[name].City, name);

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
            foreach (var item in _addressBook)
            {
                Console.WriteLine(item.Value.Display());
            }
        }

        //===================================================================//
        //------------------------[ Public Methods ]-------------------------//
        //-------------------------Search Operations----------------- -------//
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//


        public Dictionary<string, List<string>> AllContactNamesByState()
        {
            return _personByState;
        }

        public Dictionary<string, List<string>> AllContactNamesByCity()
        {
            return _personByCity;
        }

        public void DisplayContactByState( string state)
        {
            foreach (var item in _addressBook)
            {
                if(item.Value.State == state)
                    Console.WriteLine(item.Value.Display());
            }
        }

        public void DisplayContactByCity(string city)
        {
            foreach (var item in _addressBook)
            {
                if(item.Value.City == city)
                    Console.WriteLine(item.Value.Display());
            }
        }

        //===================================================================//
        //------------------------[ Public Methods ]-------------------------//
        //-------------------------Sorting Operations----------------- -------//
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        public List<string> sortedByName()
        {
            List<string> sortedName = new List<string>();
            foreach(var element in _addressBook)
            {
                sortedName.Add(element.Value.ToString());
            }
            sortedName.Sort();
            return sortedName;
        }

        //=================================================================//
        //-------------------------Private Methods-------------------------//
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        private void AddToStateDict(string state, string name)
        {
            if (this._personByState.ContainsKey(state))
                this._personByState[state].Add(name);
            else
                this._personByState.Add(state, new List<string>() { name });
        }

        [Obsolete("Using Delete and Add methods instead of Edit method", true)]
        private void EditStateDict(string state,string name)
        {
            int index;
            if (this._personByState[state].Contains(name))
            {
                index = this._personByState[state].IndexOf(name);
                this._personByState[state][index] = name;
            }
        }

        private void DeleteFromStateDict(string state, string name)
        {
            int index;
            if (this._personByState.ContainsKey(state))
            {
                if (this._personByState[state].Contains(name))
                {
                    index = this._personByState[state].IndexOf(name);
                    this._personByState[state].RemoveAt(index);
                }
            }

            if (this._personByState[state].Count == 0)
                this._personByState.Remove(state);
        }

        private void AddToCityDict(string city, string name)
        {
            if (this._personByCity.ContainsKey(city))
                this._personByCity[city].Add(name);
            else
                this._personByCity.Add(city, new List<string>() { name });
        }

        [Obsolete("Using Delete and Add methods instead of Edit method",true)]
        private void EditCityDict(string city, string name)
        {
            int index;
            if (this._personByCity[city].Contains(name))
            {
                index = this._personByCity[city].IndexOf(name);
                this._personByCity[city][index] = name;
            }
        }

        private void DeleteFromCityDict(string city, string name)
        {
            int index;
            if(this._personByCity.ContainsKey(city))
            {
                if (this._personByCity[city].Contains(name))
                {
                    index = this._personByCity[city].IndexOf(name);
                    this._personByCity[city].RemoveAt(index);
                }
            }

            if (this._personByCity[city].Count == 0)
                this._personByCity.Remove(city);
        }

    }
}
