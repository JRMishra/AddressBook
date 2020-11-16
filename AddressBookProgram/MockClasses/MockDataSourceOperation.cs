using System;
using System.Collections.Generic;
using System.Text;
using AddressBook.Interfaces;

namespace AddressBook.MockClasses
{
    class MockDataSourceOperation : IDataSourceOperation
    {
        public DictToListMapping ReadFromDataSource()
        {
            DictToListMapping dictToList = new DictToListMapping();
            dictToList.AddressBookName.Add("TestAddressBook");
            dictToList.ContactName.Add("TestContactName");
            dictToList.FirstName.Add("TestFirstName");
            dictToList.LastName.Add("TestLastName");
            dictToList.City.Add("TestCity");
            dictToList.State.Add("TestState");
            dictToList.ZipCode.Add("TestZip");
            dictToList.PhoneNumber.Add("TestPhone");
            dictToList.Email.Add("TestEmail");
            dictToList.DateAdded.Add(new DateTime(2020,01,01));
            return dictToList;
        }

        public bool WriteToDataSource(DictToListMapping dictToList)
        {
            return false;
        }
    }
}
