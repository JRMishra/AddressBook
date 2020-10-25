using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace AddressBook
{
    [Serializable]
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book");
            Console.WriteLine("========================");
            AddressBooks addressBooksCollection = new AddressBooks();
            LogDetails logDetails = new LogDetails();

            addressBooksCollection.Name = "General";

            bool contAddressBook = true; ;
            bool contContactPanel = true; ;

            do
            {
                Console.WriteLine("Enter\n" +
                "1 : To Add a new Address Book\n" +
                "2 : To use current address books ( " + addressBooksCollection.Name + " )\n" +
                "3 : Switch Address Book\n" +
                "4 : Search across all address books\n" +
                "0 : Exit");
                int userChoice;
                try
                {
                    userChoice = Int32.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    logDetails.LogDebug("Class : Program , Method : Main, Field : userChoice");
                    logDetails.LogError(e.Message + " It should be a integer");
                    userChoice = 0;
                }
                switch (userChoice)
                {
                    case 1:
                        contContactPanel = true;
                        Console.WriteLine("Add Name of the new Address Book");
                        addressBooksCollection.Name = Console.ReadLine();
                        break;
                    case 2:
                        contContactPanel = true;
                        break;
                    case 3:
                        contContactPanel = true;
                        Console.WriteLine("Enter Name of the Address Book you want to switch");
                        addressBooksCollection.Name = Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("Enter\n" +
                            "1 : Search by State\n" +
                            "2 : Search by City");
                        switch (Int32.Parse(Console.ReadLine()))
                        {
                            case 1:
                                Console.Write("Enter state name : ");
                                string state = Console.ReadLine();
                                addressBooksCollection.SearchAllAddressBooksByState(state);
                                break;
                            case 2:
                                Console.Write("Enter city name : ");
                                string city = Console.ReadLine();
                                addressBooksCollection.SearchAllAddressBooksByCity(city);
                                break;
                            default:
                                Console.WriteLine("Wrong Choice\n");
                                break;
                        }
                        break;
                    case 0:
                        contAddressBook = false;
                        contContactPanel = false;
                        break;
                    default:
                        Console.WriteLine("Wrong Option Entered");
                        break;
                }

                int choice;

                while (contContactPanel)
                {
                    Console.WriteLine("Enter\n" +
                    "1 : Add Contact Details to " + addressBooksCollection.Name + " Address Book\n" +
                    "2 : Edit a Contact Detail\n" +
                    "3 : Delete a Contact Detail\n" +
                    "4 : Search across a state\n" +
                    "5 : Sort persons\n" +
                    "0 : Exit");
                    try
                    {
                        choice = Int32.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        logDetails.LogDebug("Class : Program , Method : Main, Field : choice");
                        logDetails.LogError(e.Message + " It should be a integer");
                        choice = 0;
                    }
                    switch (choice)
                    {
                        case 1:
                            addressBooksCollection.AddNewContactInAddressBook();
                            break;
                        case 2:
                            addressBooksCollection.EditDetailsInAddressBook();
                            break;
                        case 3:
                            addressBooksCollection.DeleteOneContactDetail();
                            break;
                        case 4:
                            Console.Write("Enter State Name to search : ");
                            string stateToSearch = Console.ReadLine();
                            addressBooksCollection.SearchCurrentAddressBookByState(stateToSearch);
                            break;
                        case 5:
                            Console.WriteLine("Enter\n" +
                                "1 : Sort by name\n" +
                                "2 : Sort by city\n" +
                                "3 : Sort by state\n" +
                                "4 : Sort by Zip");
                            int userChoiceToSort = Int32.Parse(Console.ReadLine());
                            string[] property = new string[5] { "", "FirstName", "City", "State", "Zip" };
                            addressBooksCollection.SortPersonsByProperty(property[userChoiceToSort]);
                            break;
                        case 0:
                            contContactPanel = false;
                            break;
                        default:
                            break;
                    }

                }

            } while (contAddressBook);

            StoreInXmlFile(addressBooksCollection);

            return;
        }

        private static void StoreInXmlFile(AddressBooks addressBooksCollection)
        {
            string fileName = addressBooksCollection.ToString();
            string path = @"C:\Users\user\Desktop\Training-CapG\AddressBook\storage\" + fileName + ".xml";
            
            XmlWriter xmlWriter = XmlWriter.Create(path, new XmlWriterSettings { Indent = true, ConformanceLevel= ConformanceLevel.Auto});
            
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement(addressBooksCollection.ToString());
            foreach (var element in addressBooksCollection._multiAddressBooks)
            {
                xmlWriter.WriteStartElement(element.Key);
                xmlWriter.WriteAttributeString("Key", "AddressBookName");
                foreach (var item in element.Value.AddressBook)
                {
                    xmlWriter.WriteStartElement(item.Key);
                    xmlWriter.WriteAttributeString("Key", "PersonName");

                    xmlWriter.WriteStartElement("FirstName");
                    xmlWriter.WriteString(item.Value.FirstName);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("LastName");
                    xmlWriter.WriteString(item.Value.LastName);
                    xmlWriter.WriteEndElement();
                    
                    xmlWriter.WriteStartElement("City");
                    xmlWriter.WriteString(item.Value.City);
                    xmlWriter.WriteEndElement();
                    
                    xmlWriter.WriteStartElement("State");
                    xmlWriter.WriteString(item.Value.State);
                    xmlWriter.WriteEndElement();
                    
                    xmlWriter.WriteStartElement("Zip");
                    xmlWriter.WriteString(item.Value.Zip);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("PhoneNumber");
                    xmlWriter.WriteString(item.Value.PhoneNumber);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Email");
                    xmlWriter.WriteString(item.Value.Email);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        private static void ReadFromXmltFile()
        {
            string path = @"C:\Users\user\Desktop\Training-CapG\AddressBook\storage\AddressBook.AddressBooks.xml";
            string key, personName="", bookName="";

            AddressBooks addressBooks = new AddressBooks();
            AddressBookMain addressBookMain = new AddressBookMain();
            ContactDetails contactDetails = new ContactDetails();

            XmlReader xmlReader = XmlReader.Create(path);
            while(xmlReader.Read())
            {
                if(xmlReader.HasAttributes)
                {
                    key = xmlReader.GetAttribute(0);
                    if (key == "AddressBookName")
                    {
                        if(bookName.Length>0)
                        {
                            addressBookMain.AddressBook.Add(personName, contactDetails);
                            addressBooks._multiAddressBooks.Add(bookName, addressBookMain);
                        }    
                        bookName = xmlReader.ReadContentAsString();
                    }
                        
                    if(key == "PersonName")
                    {
                        if (personName.Length > 0)
                            
                        personName = xmlReader.ReadContentAsString();
                    }
                }
                else
                {
                    switch(xmlReader.Name.ToString())
                    {
                        case "FirstName":
                            contactDetails.FirstName = xmlReader.ReadContentAsString();
                            break;
                        case "LastName":
                            contactDetails.LastName = xmlReader.ReadContentAsString();
                            break;
                        case "City":
                            contactDetails.City = xmlReader.ReadContentAsString();
                            break;
                        case "State":
                            contactDetails.State = xmlReader.ReadContentAsString();
                            break;
                        case "Zip":
                            contactDetails.Zip = xmlReader.ReadContentAsString();
                            break;
                        case "PhoneNumber":
                            contactDetails.PhoneNumber = xmlReader.ReadContentAsString();
                            break;
                        case "Email":
                            contactDetails.Email = xmlReader.ReadContentAsString();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }


}
