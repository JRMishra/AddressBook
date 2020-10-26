using NLog.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AddressBook
{
    class IoOperations
    {
        public static void StoreInXmlFile(AddressBooks addressBooksCollection)
        {
            string fileName = addressBooksCollection.ToString();
            string path = @"C:\Users\user\Desktop\Training-CapG\AddressBook\storage\" + fileName + ".xml";

            XmlWriter xmlWriter = XmlWriter.Create(path, new XmlWriterSettings { Indent = true, ConformanceLevel = ConformanceLevel.Auto });

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement(addressBooksCollection.ToString());
            foreach (var element in addressBooksCollection._multiAddressBooks)
            {
                xmlWriter.WriteStartElement("AddressBookName");
                xmlWriter.WriteString(element.Key);
                foreach (var item in element.Value.AddressBook)
                {
                    xmlWriter.WriteStartElement("PersonName");
                    xmlWriter.WriteString(item.Key);

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
        
        public static void ReadFromXmlFile()
        {
            AddressBooks addressBooks = new AddressBooks();
            AddressBookMain addressBookMain = new AddressBookMain();
            ContactDetails contactDetails = new ContactDetails();

            string path = @"C:\Users\user\Desktop\Training-CapG\AddressBook\storage\sample.xml";
            XmlReader xmlReader = XmlReader.Create(path);
            string addressBookName = "";
            string personName = "";
            string element = "";

            while (xmlReader.Read())
            {
                switch(xmlReader.NodeType)
                {
                    case XmlNodeType.Document:
                        Console.WriteLine("Document : "+xmlReader.Value);
                        break;
                    case XmlNodeType.Element:
                        Console.WriteLine("Element : "+xmlReader.Value);
                        break;
                    case XmlNodeType.Text:
                        Console.WriteLine("Text : "+xmlReader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        Console.WriteLine("End Element : " + xmlReader.Value);
                        break;
                    default:
                        Console.WriteLine("Default : "+xmlReader.Name);
                        break;
                }
            }
        }

    }
}
