using AddressBook.mapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AddressBook.DBoperations
{
    class SqlServerOperation
    {
        static string path = PathToFile.ConnectionString;

        public static void ReadFromSqlServer(ref AddressBooks addressBooks)
        {
            DictToListMapping dictToList = new DictToListMapping();
            using(SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand command = new SqlCommand("select * from AddressBook", connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    dictToList.AddressBookName.Add(dataReader["AddressBookName"].ToString());
                    dictToList.ContactName.Add(dataReader["ContactName"].ToString());
                    dictToList.FirstName.Add(dataReader["FirstName"].ToString());
                    dictToList.LastName.Add(dataReader["LastName"].ToString());
                    dictToList.City.Add(dataReader["City"].ToString());
                    dictToList.State.Add(dataReader["State"].ToString());
                    dictToList.ZipCode.Add(dataReader["Zip"].ToString());
                    dictToList.PhoneNumber.Add(dataReader["Phone"].ToString());
                    dictToList.Email.Add(dataReader["Email"].ToString());
                }
            }
            addressBooks = DictToListMapping.ListToDictionary(dictToList);
        }

        public static void WriteToSqlServer(AddressBooks addressBooks)
        {
            DictToListMapping dictToList = new DictToListMapping(DictToListMapping.DictionaryToList(addressBooks));

            DataSet dataSet = new DataSet();
            dataSet.Tables[0].Columns.Add("AddressBookName");
            dataSet.Tables[0].Columns.Add("ContactName");
            dataSet.Tables[0].Columns.Add("FirstName");
            dataSet.Tables[0].Columns.Add("LastName");
            dataSet.Tables[0].Columns.Add("City");
            dataSet.Tables[0].Columns.Add("State");
            dataSet.Tables[0].Columns.Add("Zip");
            dataSet.Tables[0].Columns.Add("Phone");
            dataSet.Tables[0].Columns.Add("Email");

            for (int i = 0; i < dictToList.AddressBookName.Count; i++)
            {
                dataSet.Tables[0].Rows.Add(dictToList.AddressBookName[i]);
                dataSet.Tables[0].Rows.Add(dictToList.ContactName[i]);
                dataSet.Tables[0].Rows.Add(dictToList.FirstName[i]);
                dataSet.Tables[0].Rows.Add(dictToList.LastName[i]);
                dataSet.Tables[0].Rows.Add(dictToList.City[i]);
                dataSet.Tables[0].Rows.Add(dictToList.State[i]);
                dataSet.Tables[0].Rows.Add(dictToList.ZipCode[i]);
                dataSet.Tables[0].Rows.Add(dictToList.PhoneNumber[i]);
                dataSet.Tables[0].Rows.Add(dictToList.Email[i]);
            }

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.Update(dataSet);
        }
    }
} 
