using AddressBook.mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace AddressBook
{
    [Serializable]
    class IoOperations
    {
        public static DictToListMapping DictionaryToList(AddressBooks addressBooks)
        {
            DictToListMapping dictToList = new DictToListMapping();

            string addressBookName = "";
            foreach (var element in addressBooks._multiAddressBooks)
            {
                addressBookName = element.Key;
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

        public static AddressBooks ListToDictionary(DictToListMapping dictToList)
        {
            AddressBooks addressBooks = new AddressBooks();
            AddressBookMain addressBookMain;
            ContactDetails contactDetails;

            string addressBookName = "", contactName = "";
            for(int i=0;i<dictToList.AddressBookName.Count; i++)
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

        public static void SerializeAddressBooks(AddressBooks addressBooks)
        {
            DictToListMapping dictToList = new DictToListMapping(DictionaryToList(addressBooks));

            string path = FilePath.XmlFilePath;

            XmlSerializer xmlser = new XmlSerializer(typeof(DictToListMapping));
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            xmlser.Serialize(fileStream, dictToList);
            fileStream.Close();
        }

        public static void DeserializeAddressBooks(ref AddressBooks addressBooks)
        {
            string path = FilePath.XmlFilePath;

            DictToListMapping dictToListMapping = new DictToListMapping();

            XmlSerializer xmlser = new XmlSerializer(typeof(DictToListMapping));
            FileStream fileStream = new FileStream(path, FileMode.Open);
            dictToListMapping = (DictToListMapping)xmlser.Deserialize(fileStream);
            fileStream.Close();

            addressBooks = ListToDictionary(dictToListMapping);

        }
    }
}
