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
        Dictionary<string, AddressBookMain> _multiAddressBooks = new Dictionary<string, AddressBookMain>();

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
            if (_multiAddressBooks.ContainsKey(this._name))
                _multiAddressBooks[this._name].AddContactDetails();
            else
            {
                AddressBookMain addressBook = new AddressBookMain();
                addressBook.AddContactDetails();
                _multiAddressBooks.Add(this._name, addressBook);
            }
           
        }

        public void EditDetailsInAddressBook()
        {
            _multiAddressBooks[Name].EditContactDetails();
        }

        public void DeleteOneContactDetail()
        {
            _multiAddressBooks[Name].DeleteContactDetails();
        }

        public void DisplayContactsInCurrentAddressBook()
        {
            _multiAddressBooks[Name].DisplayAllContacts();
        }

        public void SearchByState(string state)
        {
            _multiAddressBooks[Name].DisplayContactByState(state);
        }

        public void SearchAllAddressBooksByState(string state)
        {
            foreach(var addressBook in _multiAddressBooks)
            {
                addressBook.Value.DisplayContactByState(state);
            }
        }

        public void SearchAllAddressBooksByCity(string city)
        {
            foreach(var addressBook in _multiAddressBooks)
            {
                addressBook.Value.DisplayContactByCity(city);
            }
        }
    }
}
