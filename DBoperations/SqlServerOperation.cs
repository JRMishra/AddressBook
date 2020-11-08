using AddressBook.mapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

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
    }
} 
