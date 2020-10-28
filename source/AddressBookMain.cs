using AddressBook.validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AddressBook
{
    
    public class AddressBookMain
    {
        Dictionary<string, ContactDetails> _addressBook;

        Dictionary<string, List<string>> _personByState = new Dictionary<string, List<string>>();
        
        Dictionary<string, List<string>> _personByCity = new Dictionary<string, List<string>>();
        
        LogDetails logDetails = new LogDetails();

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

        public void AddContactDetails(ContactDetails contact)
        {
            _addressBook.Add(contact.FirstName, contact);
            AddToStateDict(contact.State, contact.FirstName);
            AddToCityDict(contact.City, contact.FirstName);
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

        //------------------------[ Public Methods ]-------------------------//
        //------------------------Search Operations----------------- -------//
        
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

       //------------------------[ Public Methods ]-------------------------//
      //------------------------Sorting Operations----------------- -------//
        
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
            //Console.WriteLine($"Property : {propertyInfo.Name}");
            foreach (var element in _addressBook)
            {
                //Console.WriteLine("AddressBookMain : "+propertyInfo.GetValue(element.Value));
                if(propertyInfo.GetValue(element.Value) != null)
                    contactDetails.Add(element.Value);
            }
            contactDetails = contactDetails.OrderBy(item => propertyInfo.GetValue(item)).ToList();
            
            foreach(var contact in contactDetails)
            {
                //Console.WriteLine("2nd foreach loop : "+contact.ToString());
                sortedName.Add(contact.ToString());
            }
            return sortedName;
        }

        //=================================================================//
        //-------------------------Private Methods-------------------------//
        
        private void AddToStateDict(string state, string name)
        {
            if (this._personByState.ContainsKey(state))
                this._personByState[state].Add(name);
            else
                this._personByState.Add(state, new List<string>() { name });
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

        //Take property name as arg and store the user input in that property of Contact Detail Class
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

        //Method to manually enter contact details and validate
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

    }
}
