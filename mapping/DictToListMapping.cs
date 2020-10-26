using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    [Serializable]
    public class DictToListMapping
    {
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
    }
}
