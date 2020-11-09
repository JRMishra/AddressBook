using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace AddressBook.mapping
{
    class PathToFile
    {
        /// <summary>
        /// Path to XML File
        /// </summary>
        public static string XmlFilePath =  @"C:\Users\user\Desktop\Training-CapG\AddressBook\dataStorage\AddressBookList.xml";
        /// <summary>
        /// Path to CSV File
        /// </summary>
        public static string CsvFilePath =  @"C:\Users\user\Desktop\Training-CapG\AddressBook\dataStorage\AddressBookList.csv";
        /// <summary>
        /// Path to JSON File
        /// </summary>
        public static string JsonFilePath =  @"C:\Users\user\Desktop\Training-CapG\AddressBook\dataStorage\AddressBookList.json";
        /// <summary>
        /// Connection string for AddressBook database
        /// </summary>
        public static string ConnectionString = @"Data Source=OCAC\SQK2K20JRM;Initial Catalog=addressbook_service;Integrated Security=True";
        /// <summary>
        /// Path to JSON File created from Sql Server Db
        /// </summary>
        public static string LinqToJsonFilePath = @"C:\Users\user\Desktop\Training-CapG\AddressBook\dataStorage\AddressBookTableLinq.json";
    }
}
