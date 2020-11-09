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
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand command = new SqlCommand("select * from AddressBook", connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
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
            DeleteAllRows();

            DictToListMapping dictToList = new DictToListMapping(DictToListMapping.DictionaryToList(addressBooks));
           
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable("AddressBook");

            dataTable.Columns.Add("AddressBookName", typeof(string));
            dataTable.Columns.Add("ContactName", typeof(string));
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            dataTable.Columns.Add("City", typeof(string));
            dataTable.Columns.Add("State", typeof(string));
            dataTable.Columns.Add("Zip", typeof(string));
            dataTable.Columns.Add("Phone", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("DateAdded", typeof(DateTime));

            dataSet.Tables.Add(dataTable);
            dataSet.Tables[0].TableName = "AddressBook";
            for (int i = 0; i < dictToList.AddressBookName.Count; i++)
            {
                DataRow row = dataSet.Tables[0].NewRow();

                row[0] = dictToList.AddressBookName[i];
                row[1] = dictToList.ContactName[i];
                row[2] = dictToList.FirstName[i];
                row[3] = dictToList.LastName[i];
                row[4] = dictToList.City[i];
                row[5] = dictToList.State[i];
                row[6] = dictToList.ZipCode[i];
                row[7] = dictToList.PhoneNumber[i];
                row[8] = dictToList.Email[i];
                row[9] = DateTime.Today;

                dataSet.Tables["AddressBook"].Rows.Add(row);
            }

            using (SqlConnection connection = new SqlConnection(path))
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from AddressBook", connection);

                SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
                int rowsUpdated = dataAdapter.Update(dataSet, "AddressBook");
                Console.WriteLine("Rows Affected " + rowsUpdated);
            }

        }

        public static void DeleteAllRows()
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand command = new SqlCommand("delete from AddressBook", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
