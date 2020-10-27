using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    /*
     * This class will map the content of Address Books Dictionary to Lists
     * As, dictionaries are by default, non-serializable, this class will be serialized
     */
    [Serializable]
    public class DictToListMapping
    {
        //Default constructer
        public DictToListMapping()
        {
            AddressBookName = new List<string>();
            ContactName = new List<string>();
            FirstName = new List<string>();
            LastName = new List<string>();
            City = new List<string>();
            State = new List<string>();
            ZipCode = new List<string>();
            PhoneNumber = new List<string>();
            Email = new List<string>();
        }

        //Copy constructer
        public DictToListMapping(DictToListMapping dictToList)
        {
            AddressBookName = dictToList.AddressBookName;
            ContactName = dictToList.ContactName;
            FirstName = dictToList.FirstName;
            LastName = dictToList.LastName;
            City = dictToList.City;
            State = dictToList.State;
            ZipCode = dictToList.ZipCode;
            PhoneNumber = dictToList.PhoneNumber;
            Email = dictToList.Email;
        }

        public List<string> AddressBookName { get; set; }
        public List<string> ContactName { get; set; }
        public List<string> FirstName { get; set; }
        public List<string> LastName { get; set; }
        public List<string> City { get; set; }
        public List<string> State { get; set; }
        public List<string> ZipCode { get; set; }
        public List<string> PhoneNumber { get; set; }
        public List<string> Email { get; set; }

        //Method to map addressbooks dictionary to this class lists
        public static DictToListMapping DictionaryToList(AddressBooks addressBooks)
        {
            DictToListMapping dictToList = new DictToListMapping();

            foreach (var element in addressBooks._multiAddressBooks)
            {
                string addressBookName = element.Key;
                foreach (var contact in element.Value.AddressBook)
                {

                    dictToList.AddressBookName.Add(addressBookName);
                    dictToList.ContactName.Add(contact.Key);
                    dictToList.FirstName.Add(contact.Value.FirstName);
                    dictToList.LastName.Add(contact.Value.LastName);
                    dictToList.City.Add(contact.Value.City);
                    dictToList.State.Add(contact.Value.State);
                    dictToList.ZipCode.Add(contact.Value.Zip);
                    dictToList.PhoneNumber.Add(contact.Value.PhoneNumber);
                    dictToList.Email.Add(contact.Value.Email);
                }
            }
            return dictToList;
        }

        //Method to map this class lists to a addressbook dictionary
        public static AddressBooks ListToDictionary(DictToListMapping dictToList)
        {
            AddressBooks addressBooks = new AddressBooks();
            AddressBookMain addressBookMain;
            ContactDetails contactDetails;

            string addressBookName = "", contactName = "";
            for (int i = 0; i < dictToList.AddressBookName.Count; i++)
            {
                addressBookMain = new AddressBookMain();
                contactDetails = new ContactDetails();

                addressBookName = dictToList.AddressBookName[i];
                contactName = dictToList.ContactName[i];
                contactDetails.FirstName = dictToList.FirstName[i];
                contactDetails.LastName = dictToList.LastName[i];
                contactDetails.City = dictToList.City[i];
                contactDetails.State = dictToList.State[i];
                contactDetails.Zip = dictToList.ZipCode[i];
                contactDetails.PhoneNumber = dictToList.PhoneNumber[i];
                contactDetails.Email = dictToList.Email[i];

                if (addressBooks._multiAddressBooks.ContainsKey(addressBookName))
                    addressBooks._multiAddressBooks[addressBookName].AddressBook.Add(contactName, contactDetails);
                else
                {
                    addressBookMain.AddressBook.Add(contactName, contactDetails);
                    addressBooks._multiAddressBooks.Add(addressBookName, addressBookMain);
                }
            }
            return addressBooks;
        }
    }
}
