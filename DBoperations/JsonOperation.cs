using AddressBook.mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AddressBook.DBoperations
{
    class JsonOperation
    {
        public static void WriteToJson(AddressBooks addressBooks)
        {
            DictToListMapping dictToList = new DictToListMapping(IoOperations.DictionaryToList(addressBooks));
            string path = FilePath.JsonFilePath;

            string jsonData = JsonConvert.SerializeObject(dictToList);
            File.WriteAllText(path, jsonData);
        }

        public static void ReadFromJson(ref AddressBooks addressBooks)
        {
            string path = FilePath.JsonFilePath;
            DictToListMapping dictToList = new DictToListMapping();

            string jsonData = File.ReadAllText(path);
            dictToList = JsonConvert.DeserializeObject<DictToListMapping>(jsonData);

            addressBooks = IoOperations.ListToDictionary(dictToList);
        }
    }
}
