using AddressBook.mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AddressBook.Interfaces;

namespace AddressBook.DBoperations
{
    public class JsonOperation : IDataSourceOperation
    {
        static string path = PathToFile.JsonFilePath;

        /// <summary>
        /// Write addressbooks to JSON file
        /// </summary>
        /// <param name="addressBooks"></param>
        public static void WriteToJson(AddressBooks addressBooks)
        {
            DictToListMapping dictToList = new DictToListMapping(DictToListMapping.DictionaryToList(addressBooks));
            
            string jsonData = JsonConvert.SerializeObject(dictToList);
            File.WriteAllText(path, jsonData);
        }

        /// <summary>
        /// Read from JSON file to store in address books
        /// </summary>
        /// <param name="addressBooks"></param>
        public static void ReadFromJson(ref AddressBooks addressBooks)
        {
            DictToListMapping dictToList = new DictToListMapping();

            string jsonData = File.ReadAllText(path);
            dictToList = JsonConvert.DeserializeObject<DictToListMapping>(jsonData);

            addressBooks = DictToListMapping.ListToDictionary(dictToList);
        }

        public DictToListMapping ReadFromDataSource()
        {
            DictToListMapping dictToList = new DictToListMapping();
            string jsonData = File.ReadAllText(path);
            dictToList = JsonConvert.DeserializeObject<DictToListMapping>(jsonData);
            return dictToList;
        }

        public bool WriteToDataSource(DictToListMapping dictToList)
        {
            throw new NotImplementedException();
        }
    }
}
