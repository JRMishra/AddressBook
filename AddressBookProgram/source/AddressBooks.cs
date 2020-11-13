namespace AddressBook
{
    using System;
    using System.Collections.Generic;

    public class AddressBooks
    {
        string _name;
        public string Name { get => _name; set => _name = value; }
        public Dictionary<string, AddressBookMain> _multiAddressBooks = new Dictionary<string, AddressBookMain>();
        
        public AddressBooks()
        {
            this._name = "General";
        }

        //------------------------[ Public Methods ]-------------------------//
        //--------------------------CRUD Operations--------------------------//

        /// <summary>
        /// Add new contact details in address book
        /// </summary>
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

        /// <summary>
        /// Edit contact details of a person in address book
        /// </summary>
        public void EditDetailsInAddressBook() => _multiAddressBooks[Name].EditContactDetails();

        /// <summary>
        /// Delete one contact detail present in address book
        /// </summary>
        public void DeleteOneContactDetail() => _multiAddressBooks[Name].DeleteContactDetails();

        /// <summary>
        /// Display contact details in current address book
        /// </summary>
        public void DisplayContactsInCurrentAddressBook() => _multiAddressBooks[Name].DisplayAllContacts();

        //------------------------[ Public Methods ]-------------------------//
        //------------------------Display Operations-------------------------//

        /// <summary>
        /// Search contact details of a state accross current address books
        /// </summary>
        /// <param name="state">State name to search for</param>
        public void SearchCurrentAddressBookByState(string state) => _multiAddressBooks[Name].DisplayContactByState(state);

        /// <summary>
        /// Search contact details of a state accross all address books
        /// </summary>
        /// <param name="state">State name to search for</param>
        public void SearchAllAddressBooksByState(string state)
        {
            foreach (var addressBook in _multiAddressBooks)
            {
                addressBook.Value.DisplayContactByState(state);
            }
        }

        /// <summary>
        /// Search contact details of a city in current address book
        /// </summary>
        /// <param name="city">City name to search for</param>
        public void SearchCurrentAddressBookByCity(string city) => _multiAddressBooks[Name].DisplayContactByCity(city);

        /// <summary>
        /// Search contact details by city accross all address books
        /// </summary>
        /// <param name="city">Name of city to search</param>
        public void SearchAllAddressBooksByCity(string city)
        {
            foreach (var addressBook in _multiAddressBooks)
            {
                addressBook.Value.DisplayContactByCity(city);
            }
        }

       //------------------------Sorting Operations-------------------------//

        /// <summary>
        /// To sort person names in a address book by any property
        /// </summary>
        /// <param name="property">Property name to sort by</param>
        public void SortPersonsByProperty(string property)
        {
            if (_multiAddressBooks.ContainsKey(_name))
            {
                List<string> sortedPersonsByProperty = _multiAddressBooks[_name].SortedByProperty(property);
                if (sortedPersonsByProperty.Count > 0)
                {
                    Console.WriteLine($"Contacts after sorting by {property}");
                    foreach (string person in sortedPersonsByProperty)
                        Console.WriteLine(person);
                }
            }
            else
            {
                Console.WriteLine($"{_name} address book is empty");
            }
        }

        //=========================[ State Profile ]=====================//

        /// <summary>
        /// Group & Display list of person names by their state
        /// </summary>
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

        /// <summary>
        /// Count of persons present by state for all states
        /// </summary>
        /// <returns>Dictionary containing state names ans person count for them</returns>
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

        /// <summary>
        /// Display count of contacts by their state
        /// </summary>
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

        /// <summary>
        /// Group & Display list of person names by their city
        /// </summary>
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

        /// <summary>
        /// Count of persons present by city for all cities
        /// </summary>
        /// <returns>Dictionary containing city names ans person count for them</returns>
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

        /// <summary>
        /// Display count of contacts by their city
        /// </summary>
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

        //-------------------------Private Methods-------------------------//
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        /// <summary>
        /// To merge dictionaries, having state names and list of persons for respective states, 
        /// from all address books into one dictionary
        /// </summary>
        /// <returns>merged dictionary</returns>
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

        /// <summary>
        /// To merge dictionaries( having city names and list of persons for respective cities)
        /// from all address books into one dictionary
        /// </summary>
        /// <returns>merged dictionary</returns>
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
