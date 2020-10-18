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
        //public Dictionary<string, List<string>> detailsOfAllByState = new Dictionary<string, List<string>>();

        public AddressBooks()
        {
            this._name = "General";
        }

        public AddressBooks(string name)
        {
            this._name = name;
        }

        public string Name { get => _name; set => _name = value; }

           //===================================================================//
          //------------------------[ Public Methods ]-------------------------//
         //--------------------------CRUD Operations--------------------------//
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        //Add new contact details in address book
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

        //Edit contact details of a person in address book
        public void EditDetailsInAddressBook()
        {
            _multiAddressBooks[Name].EditContactDetails();
        }

        //Delete a contact detail
        public void DeleteOneContactDetail()
        {
            _multiAddressBooks[Name].DeleteContactDetails();
        }

        //Display contact details in current address book
        public void DisplayContactsInCurrentAddressBook()
        {
            _multiAddressBooks[Name].DisplayAllContacts();
        }

           //===================================================================//
          //------------------------[ Public Methods ]-------------------------//
         //------------------------Display Operations-------------------------//
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        //Search contact details of a state in current address book
        public void SearchCurrentAddressBookByState(string state)
        {
            _multiAddressBooks[Name].DisplayContactByState(state);
        }

        //Search contact details of a state accross all address books
        public void SearchAllAddressBooksByState(string state)
        {
            foreach (var addressBook in _multiAddressBooks)
            {
                addressBook.Value.DisplayContactByState(state);
            }
        }

        //Search contact details of a city in current address book
        public void SearchCurrentAddressBookByCity(string city)
        {
            _multiAddressBooks[Name].DisplayContactByCity(city);
        }

        //Search contact details of a city accross all address books
        public void SearchAllAddressBooksByCity(string city)
        {
            foreach (var addressBook in _multiAddressBooks)
            {
                addressBook.Value.DisplayContactByCity(city);
            }
        }

        //=========================[ State Profile ]=====================//

        public void DisplayPersonNameByState()
        {
            Dictionary<string, List<string>> personsByState = new Dictionary<string, List<string>>();
            personsByState = SearchPersonsByState();
            foreach (var items in personsByState)
            {
                Console.WriteLine("State : " + items.Key);
                Console.Write("Persons : ");
                foreach (string person in items.Value)
                {
                    Console.Write(person+" , ");
                }
                Console.WriteLine();
            }
        }

        public Dictionary<string,int> CountPersonsByState()
        {
            Dictionary<string, int> count = new Dictionary<string, int>();

            Dictionary<string, List<string>> personsByState = new Dictionary<string, List<string>>();
            personsByState = SearchPersonsByState();
            foreach (var items in personsByState)
            {
                count.Add(items.Key, items.Value.Count);
            }

            return count;
        }

        public void DisplayPersonCountByState()
        {
            Dictionary<string, int> countByState = new Dictionary<string, int>();
            countByState = CountPersonsByState();

            Console.WriteLine("State     Count");
            Console.WriteLine("_________________");
            foreach (var item in countByState)
            {
                Console.WriteLine(item.Key + "     " + item.Value);
            }
            Console.WriteLine();
        }

        //======================[ City Profile ]========================//

        public void DisplayPersonNameByCity()
        {
            Dictionary<string, List<string>> personsByCity = new Dictionary<string, List<string>>();
            personsByCity = SearchPersonsByCity();
            foreach (var items in personsByCity)
            {
                Console.WriteLine("City : " + items.Key);
                Console.WriteLine("Persons : ");
                foreach (string person in items.Value)
                {
                    Console.WriteLine(person+" , ");
                }
            }
        }

        public Dictionary<string, int> CountPersonsByCity()
        {
            Dictionary<string, int> count = new Dictionary<string, int>();

            Dictionary<string, List<string>> personsByCity = new Dictionary<string, List<string>>();
            personsByCity = SearchPersonsByCity();
            foreach (var items in personsByCity)
            {
                count.Add(items.Key, items.Value.Count);
            }

            return count;
        }

        public void DisplayPersonCountByCity()
        {
            Dictionary<string, int> countByCity = new Dictionary<string, int>();
            countByCity = CountPersonsByCity();

            Console.WriteLine("City     Count");
            Console.WriteLine("_________________");
            foreach (var item in countByCity)
            {
                Console.WriteLine(item.Key + "     " + item.Value);
            }
        }

        //=================================================================//
        //-------------------------Private Methods-------------------------//
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        private Dictionary<string, List<string>> SearchPersonsByState()
        {
            Dictionary<string, List<string>> detailsOfAllByState = new Dictionary<string, List<string>>();
            foreach (var addressBook in _multiAddressBooks)
            {
                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
                dict = addressBook.Value.AllContactNamesByState();
                foreach (var item in dict)
                {
                    if (detailsOfAllByState.ContainsKey(item.Key))
                        detailsOfAllByState[item.Key].AddRange(item.Value);
                    else
                        detailsOfAllByState.Add(item.Key, item.Value);
                }
            }
            return detailsOfAllByState;
        }

        private Dictionary<string, List<string>> SearchPersonsByCity()
        {
            Dictionary<string, List<string>> detailsOfAllByCity = new Dictionary<string, List<string>>();
            foreach (var addressBook in _multiAddressBooks)
            {
                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
                dict = addressBook.Value.AllContactNamesByCity();
                foreach (var item in dict)
                {
                    if (detailsOfAllByCity.ContainsKey(item.Key))
                        detailsOfAllByCity[item.Key].AddRange(item.Value);
                    else
                        detailsOfAllByCity.Add(item.Key, item.Value);
                }
            }
            return detailsOfAllByCity;
        }

    }
}
