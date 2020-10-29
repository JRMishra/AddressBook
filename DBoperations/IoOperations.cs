using AddressBook.mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace AddressBook
{
    class IoOperations
    {

        static string path = PathToFile.XmlFilePath;

        /// <summary>
        /// XML Serialize instance of an AddressBooks Class
        /// </summary>
        /// <param name="addressBooks">instance to serialize</param>
        public static void SerializeAddressBooks(AddressBooks addressBooks)
        {
            DictToListMapping dictToList = new DictToListMapping(DictToListMapping.DictionaryToList(addressBooks));

            XmlSerializer xmlser = new XmlSerializer(typeof(DictToListMapping));
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            xmlser.Serialize(fileStream, dictToList);
            fileStream.Close();
        }

        /// <summary>
        /// XML Deserialize and store in instance of AddressBooks Class
        /// </summary>
        /// <param name="addressBooks">ref to addressbooks instance</param>
        public static void DeserializeAddressBooks(ref AddressBooks addressBooks)
        {
            DictToListMapping dictToListMapping = new DictToListMapping();

            XmlSerializer xmlser = new XmlSerializer(typeof(DictToListMapping));
            FileStream fileStream = new FileStream(path, FileMode.Open);
            dictToListMapping = (DictToListMapping)xmlser.Deserialize(fileStream);
            fileStream.Close();

            addressBooks = DictToListMapping.ListToDictionary(dictToListMapping);

        }
    }
}
