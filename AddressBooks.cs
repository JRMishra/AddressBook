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

        public void AddNewContactInAddressBook()
        {
            if (_multContactDetails.ContainsKey(this._name))
                _multContactDetails[this._name].AddContactDetails();
            else
            {
                AddressBookMain addressBook = new AddressBookMain();
                addressBook.AddContactDetails();
                _multContactDetails.Add(this._name, addressBook);
            }
           
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
