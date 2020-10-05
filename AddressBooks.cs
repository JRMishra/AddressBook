using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    class AddressBooks
    {
        LogDetails logDetails = new LogDetails();
        string _name;
        Dictionary<string, AddressBookMain> _multContactDetails = new Dictionary<string, AddressBookMain>();

        public AddressBooks()
        {
            this._name = "General";
        }

        public AddressBooks(string name)
        {
            this._name = name;
        }

        public string Name { get => _name; set => _name = value; }

        public void AddNewAddressBook()
        {
            AddressBookMain addressBook = new AddressBookMain();
            int numDetails;
            Console.Write("Enter number of Contact Details you want to save : ");
            try
            {
                numDetails = Int32.Parse(Console.ReadLine());
            }
            catch(Exception e)
            {
                logDetails.LogDebug("Class : AddressBooks , Method : AddNewAddressBook, Field : numDetails");
                logDetails.LogError(e.Message+" It should be a integer");
                numDetails = 0;
            }

            for (int i = 1; i <= numDetails; i++)
                addressBook.AddContactDetails();

            _multContactDetails.Add(this._name, addressBook);
        }

        public void EditDetailsInAddressBook()
        {
            _multContactDetails[Name].EditContactDetails();
        }

        public void DeleteOneContactDetail()
        {
            _multContactDetails[Name].DeleteContactDetails();
        }

        public void DisplayContactsInCurrentAddressBook()
        {
            _multContactDetails[Name].DisplayAllContacts();
        }
    }
}
