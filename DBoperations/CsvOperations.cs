using AddressBook.mapping;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;

namespace AddressBook.DBoperations
{
    class CsvOperations
    {
        public static void WriteToCsv(AddressBooks addressBooks)
        {
            DictToListMapping dictToList = new DictToListMapping(IoOperations.DictionaryToList(addressBooks));
            string path = FilePath.CsvFilePath;
            List<string[]> records = new List<string[]>();
            string[] person;

            for (int i = 0; i < dictToList.AddressBookName.Count; i++)
            {
                person = new string[9];

                person[0] = dictToList.AddressBookName[i];
                person[1] = dictToList.ContactName[i];
                person[2] = dictToList.FirstName[i];
                person[3] = dictToList.LastName[i];
                person[4] = dictToList.City[i];
                person[5] = dictToList.State[i];
                person[6] = dictToList.ZipCode[i];
                person[7] = dictToList.PhoneNumber[i];
                person[8] = dictToList.Email[i];

                records.Add(person);
            }

            Type type = dictToList.GetType();

            PropertyInfo[] propertyInfo = type.GetProperties();

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Join(",", propertyInfo.Select(p => p.Name)));
                for (int i = 0; i < records.Count; i++)
                {
                    writer.WriteLine(string.Join(",", records[i]));
                }
            }

        }

        public static void ReadFromCsv(ref AddressBooks addressBooks)
        {
            string path = FilePath.CsvFilePath;
            DictToListMapping dictToListMapping = new DictToListMapping();

            var lines = File.ReadAllLines(path);
            var csv = from line in lines
                        select (line.Split('\n')).ToArray();

            DictToListMapping dictToList = new DictToListMapping();
            int index = 0;
            foreach(var words in csv)
            {
                if(index==0)
                {
                    index++;
                    continue;
                }
                var values = words[0].Split(',');

                dictToList.AddressBookName.Add(values[0]);
                dictToList.ContactName.Add(values[1]);
                dictToList.FirstName.Add(values[2]);
                dictToList.LastName.Add(values[3]);
                dictToList.City.Add(values[4]);
                dictToList.State.Add(values[5]);
                dictToList.ZipCode.Add(values[6]);
                dictToList.PhoneNumber.Add(values[7]);
                dictToList.Email.Add(values[8]);

                index++;
            }

            addressBooks = IoOperations.ListToDictionary(dictToList);
        }
    }
}
