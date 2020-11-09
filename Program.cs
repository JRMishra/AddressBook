namespace AddressBook
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using AddressBook.DBoperations;
    using AddressBook.source;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book");
            Console.WriteLine("========================");

            //ProgramStartUp.Start();
            List<DataRow> data = SqlServerOperation.ContactsAddedBetweenDateRange(new DateTime(2020, 10, 01), DateTime.Today);
            foreach(DataRow row in data)
            {
                Console.WriteLine(row.Field<string>("ContactName"));
            }
            //Console.ReadKey();
        }
    }
}
