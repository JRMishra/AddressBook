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

            ProgramStartUp.Start();
            //ProgramStartUp.LinqOperations();
            Console.ReadKey();
        }
    }
}
