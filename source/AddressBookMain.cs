namespace AddressBook
{
    using AddressBook.validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class AddressBookMain
    {
        Dictionary<string, ContactDetails> _addressBook;
        Dictionary<string, List<string>> _personByState = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> _personByCity = new Dictionary<string, List<string>>();
        
        public Dictionary<string, ContactDetails> AddressBook { get => _addressBook; set => _addressBook = value; }

        public AddressBookMain()
        {
            this._addressBook = new Dictionary<string, ContactDetails>();
        }

        public AddressBookMain(Dictionary<string,ContactDetails> contactAddress)
        {
            this._addressBook = contactAddress;
        }

        //------------------------[ Public Methods ]-------------------------//
       //--------------------------CRUD Operations--------------------------//
        
        /// <summary>
        /// Manually add contact details to an address book
        /// </summary>
        public void AddContactDetails()
        {
            ContactDetails contact = new ContactDetails();
            string saveNameAs= EnterContactDetailsManually(ref contact);

            _addressBook.Add(saveNameAs, contact);
            AddToStateDict(contact.State, contact.FirstName);
            AddToCityDict(contact.City, contact.FirstName);

            Console.WriteLine("Processing...\n");
            Console.WriteLine("Details saved successfully");
            return;
        }

        /// <summary>
        /// Take contact details as arg and add it to an address book
        /// </summary>
        /// <param name="contact">Contact detail to add</param>
        public void AddContactDetails(ContactDetails contact)
        {
            _addressBook.Add(contact.FirstName, contact);
            AddToStateDict(contact.State, contact.FirstName);
            AddToCityDict(contact.City, contact.FirstName);
        }

        /// <summary>
        /// Edit details of an existing contact
        /// </summary>
        public void EditContactDetails()
        {
            Console.WriteLine("Enter First Name whose details need to be edited ");
            string name = Console.ReadLine();
            if(_addressBook.ContainsKey(name))
            {
                bool notCompleted = true;
                Console.WriteLine("Enter\n" +
                        "1 : Edit City\n" +
                        "2 : Edit State\n" +
                        "3 : Edit Zip\n" +
                        "4 : Edit Phone Number\n" +
                        "5 : Edit Email ID\n" +
                        "0 : Edit Completed");
                while (notCompleted)
                {
                    string choice = Console.ReadLine();
                    if (choice != "0")
                    {
                        EditContactDetailsSwitch(name, choice);
                        Console.WriteLine("\nIf there is anything else to edit, enter respective number\n" + "else enter 0 to exit");
                    }
                    else
                        notCompleted = false;
                }

            }
            else
            {
                Console.WriteLine("Details of " + name + " is not present");
            }
        }

        /// <summary>
        /// Delete a contact detail
        /// </summary>
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

        /// <summary>
        /// Display all contact details
        /// </summary>
        public void DisplayAllContacts()
        {
            Console.WriteLine("All Contacts are :");
            foreach (var item in _addressBook)
            {
                Console.WriteLine(item.Value.Display());
            }
        }

        //------------------------[ Public Methods ]-------------------------//
        //------------------------Search Operations----------------- -------//

        /// <summary>
        /// Retrieve state names and list of contacts from respective states
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> AllContactNamesByState()
        {
            return _personByState;
        }

        /// <summary>
        /// Retrieve city names and list of contacts from respective cities
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> AllContactNamesByCity()
        {
            return _personByCity;
        }

        /// <summary>
        /// Display list of contacts in an address book from specified state
        /// </summary>
        /// <param name="state">State name to display contacts</param>
        public void DisplayContactByState( string state)
        {
            foreach (var item in _addressBook)
            {
                if(item.Value.State == state)
                    Console.WriteLine(item.Value.Display());
            }
        }

        /// <summary>
        /// Display list of contacts in an address book from specified city
        /// </summary>
        /// <param name="city">City name to display contacts</param>
        public void DisplayContactByCity(string city)
        {
            foreach (var item in _addressBook)
            {
                if(item.Value.City == city)
                    Console.WriteLine(item.Value.Display());
            }
        }

       //------------------------[ Public Methods ]-------------------------//
      //------------------------Sorting Operations----------------- -------//
        
        /// <summary>
        /// Retrieve contact names sorted by first name
        /// </summary>
        /// <returns></returns>
        public List<string> SortedByName()
        {
            List<string> sortedName = new List<string>();
            foreach(var element in _addressBook)
            {
                sortedName.Add(element.Value.ToString());
            }
            sortedName.Sort();
            return sortedName;
        }

        /// <summary>
        /// Retrieve contact names sorted by any property, if exists
        /// </summary>
        /// <param name="property">Property name to sort</param>
        /// <returns>Returns list of names sorted by specified property</returns>
        public List<string> SortedByProperty(string property)
        {
            List<ContactDetails> contactDetails = new List<ContactDetails>();
            List<string> sortedName = new List<string>();

            Type typeRef;
            typeRef = Type.GetType("AddressBook.ContactDetails");
            PropertyInfo propertyInfo = typeRef.GetProperty(property);
            if (propertyInfo == null)
            {
                Console.WriteLine("wrong property name");
                return sortedName;
            }

            foreach (var element in _addressBook)
            {
                if(propertyInfo.GetValue(element.Value) != null)
                    contactDetails.Add(element.Value);
            }
            contactDetails = contactDetails.OrderBy(item => propertyInfo.GetValue(item)).ToList();
            
            foreach(var contact in contactDetails)
            {
                sortedName.Add(contact.ToString());
            }
            return sortedName;
        }

        //=================================================================//
        //-------------------------Private Methods-------------------------//
        
        /// <summary>
        /// Add name to dictionary having state names and list of persons for respective states
        /// </summary>
        private void AddToStateDict(string state, string name)
        {
            if (this._personByState.ContainsKey(state))
                this._personByState[state].Add(name);
            else
                this._personByState.Add(state, new List<string>() { name });
        }

        /// <summary>
        /// delete name from dictionary having state names and list of persons for respective states
        /// </summary>
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

        /// <summary>
        /// Add name to dictionary having city names and list of persons for respective cities
        /// </summary>
        private void AddToCityDict(string city, string name)
        {
            if (this._personByCity.ContainsKey(city))
                this._personByCity[city].Add(name);
            else
                this._personByCity.Add(city, new List<string>() { name });
        }

        /// <summary>
        /// Delete name from dictionary having city names and list of persons for respective cities
        /// </summary>
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

        /// <summary>
        /// Take ContactDetails class property name as arg and 
        /// validate user input with rules associated with respective property
        /// </summary>
        /// <param name="propertyName">Property name to store user input</param>
        /// <returns>Valid user input for respective property</returns>
        private string TakeUserInput(string propertyName)
        {
            Type typeContact = typeof(ContactDetails);
            PropertyInfo contactProperty = typeContact.GetProperty(propertyName,BindingFlags.Public|BindingFlags.Instance);
            Type typeValidation = typeof(ValidateContact);
            PropertyInfo validationProperty = typeValidation.GetProperty(propertyName + "Pattern", BindingFlags.Public | BindingFlags.Static);
            Type typeRule = typeof(ValidationRules);
            PropertyInfo ruleProperty = typeRule.GetProperty(propertyName+"Rule",BindingFlags.Public | BindingFlags.Static);
            
            string propValue;
            bool status;
            do
            {
                Console.Write(contactProperty.Name + " : ");
                propValue = Console.ReadLine();
                status = Regex.IsMatch(propValue,validationProperty.GetValue(null,null).ToString());
                if(!status)
                {
                    Console.WriteLine("Invalid !!");
                    Console.WriteLine(ruleProperty.GetValue(null).ToString());
                }
            } while (!status);
            
            return propValue;
        }

        /// <summary>
        /// Manually enter contact details to store in an address book
        /// </summary>
        /// <param name="contact">ref to instance of ContactDetails class to input value</param>
        /// <returns>Contact name to save as</returns>
        private string EnterContactDetailsManually(ref ContactDetails contact)
        {
            Console.WriteLine("Enter\n");
            contact.FirstName = TakeUserInput("FirstName");
            contact.LastName = TakeUserInput("LastName");
            contact.City = TakeUserInput("City");
            contact.State = TakeUserInput("State");
            contact.Zip = TakeUserInput("Zip");
            contact.PhoneNumber = TakeUserInput("PhoneNumber");
            contact.Email = TakeUserInput("Email");
            contact.DateAdded = DateTime.Today;
            Console.WriteLine();

            int i = 1;
            string contactName = contact.FirstName + contact.LastName;
            while(_addressBook.ContainsKey(contactName))
            {
                contactName += i;
                i++;
            }
            return contactName;
        }

        /// <summary>
        /// Switch cases to edit details of an existing contact
        /// </summary>
        /// <param name="name">Name of contact</param>
        /// <param name="choice">Property (Other than names) to edit</param>
        private void EditContactDetailsSwitch(string name, string choice)
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Edit Updated City :");
                    DeleteFromCityDict(_addressBook[name].City, name);
                    _addressBook[name].City = Console.ReadLine();
                    AddToCityDict(_addressBook[name].City, name);
                    break;
                case "2":
                    Console.Write("Edit Updated State :");
                    DeleteFromStateDict(_addressBook[name].State, name);
                    _addressBook[name].State = Console.ReadLine();
                    AddToStateDict(_addressBook[name].State, name);
                    break;
                case "3":
                    Console.Write("Edit Updated Zip :");
                    _addressBook[name].Zip = Console.ReadLine();
                    break;
                case "4":
                    Console.Write("Edit Updated Phone Number :");
                    _addressBook[name].PhoneNumber = Console.ReadLine();
                    break;
                case "5":
                    Console.Write("Edit Updated Email Id :");
                    _addressBook[name].State = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Wrong Choice\nChoose Again");
                    break;
            }
        }

    }
}
