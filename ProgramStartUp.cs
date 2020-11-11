namespace AddressBook.source
{
    using AddressBook.DBoperations;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    class ProgramStartUp
    {
        /// <summary>
        /// Start AddressBook Program to perform various operations
        /// </summary>
        public static void Start()
        {
            AddressBooks addressBooksCollection = new AddressBooks();

            //FileReadingOperation(ref addressBooksCollection);
            Task readingTask = new Task(() =>
            {
                FileReadingOperation(ref addressBooksCollection);
            });
            readingTask.Start();
            readingTask.Wait();

            addressBooksCollection.Name = "General";
            bool contAddressBook = true; ;
            bool contContactPanel = true; ;
            do
            {
                Console.WriteLine("Enter\n" +
                "1 : To Add a new Address Book\n" +
                "2 : To use current address books ( " + addressBooksCollection.Name + " )\n" +
                "3 : Switch Address Book\n" +
                "4 : Search across all address books\n" +
                "0 : Exit");

                string userChoice = Console.ReadLine();
                if(userChoice!="0")
                {
                    AddressBookChoiceSwitch(ref addressBooksCollection, userChoice);
                    contContactPanel = true;
                }  
                else
                {
                    contAddressBook = false;
                    contContactPanel = false;
                }
                while (contContactPanel)
                {
                    Console.WriteLine("Enter\n" +
                    "1 : Add Contact Details to " + addressBooksCollection.Name + " Address Book\n" +
                    "2 : Edit a Contact Detail\n" +
                    "3 : Delete a Contact Detail\n" +
                    "4 : Search across a state\n" +
                    "5 : Sort persons\n" +
                    "0 : Exit");

                    string choice = Console.ReadLine();
                    if(choice!="0")
                        AddressBookOperationSwitch(ref addressBooksCollection, choice);
                    else
                    {
                        contContactPanel = false;
                    }
                }

            } while (contAddressBook);

            //FileWritingOperation(addressBooksCollection);
            Task writingTask = new Task(() =>
            {
                FileWritingOperation(addressBooksCollection);
            });
            writingTask.Start();
            writingTask.Wait();
            Console.WriteLine("Saved changes to database");
        }

        public static void LinqOperations()
        {
            //1
            List<DataRow> data = SqlServerOperation.ContactsAddedBetweenDateRange(new DateTime(2020, 11, 01), DateTime.Today);
            Console.WriteLine("Contacts added between 01/11/2020 and Today ");
            foreach (DataRow row in data)
            {
                Console.WriteLine(row.Field<string>("ContactName"));
            }
            Console.WriteLine();
            //2
            Dictionary<string,int> countByCity = SqlServerOperation.CountByCity();
            Console.WriteLine("City\tCount");
            foreach(var item in countByCity)
            {
                Console.WriteLine(item.Key+"\t"+item.Value);
            }
            Console.WriteLine();
            //3
            Dictionary<string, int> countByState = SqlServerOperation.CountByState();
            Console.WriteLine("State\tCount");
            foreach (var item in countByState)
            {
                Console.WriteLine(item.Key + "\t" + item.Value);
            }
            Console.WriteLine();
            //4
            //ContactDetails contact = new ContactDetails("Shyam", "Kumar", "Barauni", "Patna", "710923", "9309165207", "kshyam@mymail.com");
            //SqlServerOperation.AddContactDetail("College", "ShyamBmep", contact);

            //5
            SqlServerOperation.WriteFromSqlServerToJson();
        }
        //----------------------------------- [ Private Methods ]----------------------------------------//
        /// <summary>
        /// Write contact details of all address books in XML, CSV & JSON file
        /// </summary>
        /// <param name="addressBooksCollection"></param>
        private static void FileWritingOperation(AddressBooks addressBooksCollection)
        {
            LogDetails logDetails = new LogDetails();
            try
            {
                //IoOperations.SerializeAddressBooks(addressBooksCollection);
                //CsvOperations.WriteToCsv(addressBooksCollection);
                //JsonOperation.WriteToJson(addressBooksCollection);
                SqlServerOperation.WriteToSqlServer(addressBooksCollection);
            }
            catch (Exception e)
            {
                Console.WriteLine("In catch block : "+e.Message);
                logDetails.LogDebug("IO Error in Writing operation");
                logDetails.LogError(e.Message);
            }
        }

        /// <summary>
        /// Read contact details from XML/ CSV / JSON file
        /// and store in AddressBooks class instance
        /// </summary>
        /// <param name="addressBooksCollection">reference to addressbooks instance to store contact details</param>
        private static void FileReadingOperation(ref AddressBooks addressBooksCollection)
        {
            LogDetails logDetails = new LogDetails();
            try
            {
                //IoOperations.DeserializeAddressBooks(ref addressBooksCollection);
                //CsvOperations.ReadFromCsv(ref addressBooksCollection);
                //JsonOperation.ReadFromJson(ref addressBooksCollection);
                SqlServerOperation.ReadFromSqlServer(ref addressBooksCollection);
            }
            catch (Exception e)
            {
                logDetails.LogDebug("IO Error in Reading operation");
                logDetails.LogError(e.Message);
            }
        }

        /// <summary>
        /// Switch Cases for address books operation
        /// </summary>
        /// <param name="addressBooksCollection">addressbooks instance to perform operations</param>
        /// <param name="choice">User input to choose operation among available options</param>
        /// <returns></returns>
        private static void AddressBookOperationSwitch(ref AddressBooks addressBooksCollection, string choice)
        {
            LogDetails logDetails = new LogDetails();
            switch (choice)
            {
                case "1":
                    addressBooksCollection.AddNewContactInAddressBook();
                    break;
                case "2":
                    addressBooksCollection.EditDetailsInAddressBook();
                    break;
                case "3":
                    addressBooksCollection.DeleteOneContactDetail();
                    break;
                case "4":
                    Console.Write("Enter State Name to search : ");
                    string stateToSearch = Console.ReadLine();
                    addressBooksCollection.SearchCurrentAddressBookByState(stateToSearch);
                    break;
                case "5":
                    Console.WriteLine("Enter\n" +
                        "1 : Sort by name\n" +
                        "2 : Sort by city\n" +
                        "3 : Sort by state\n" +
                        "4 : Sort by Zip");
                    int userChoiceToSort;
                    try
                    {
                        userChoiceToSort = Int32.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        logDetails.LogDebug("Error in sorting section");
                        logDetails.LogError(e.Message);
                        Console.WriteLine("Wrong Choice\nSorting by Name(default)");
                        userChoiceToSort = 1;
                    }
                    string[] property = new string[5] { "", "FirstName", "City", "State", "Zip" };
                    addressBooksCollection.SortPersonsByProperty(property[userChoiceToSort]);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Switch Cases to add / choose/ other operations involving all address books
        /// </summary>
        /// <param name="addressBooksCollection">ref to all address books</param>
        /// <param name="userChoice">User input to choose operation among available options</param>
        private static void AddressBookChoiceSwitch(ref AddressBooks addressBooksCollection, string userChoice)
        {
            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("Add Name of the new Address Book");
                    addressBooksCollection.Name = Console.ReadLine();
                    break;
                case "2":
                    break;
                case "3":
                    Console.WriteLine("Enter Name of the Address Book you want to switch");
                    addressBooksCollection.Name = Console.ReadLine();
                    break;
                case "4":
                    Console.WriteLine("Enter\n" +
                        "1 : Search by State\n" +
                        "2 : Search by City");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.Write("Enter state name : ");
                            string state = Console.ReadLine();
                            addressBooksCollection.SearchAllAddressBooksByState(state);
                            break;
                        case "2":
                            Console.Write("Enter city name : ");
                            string city = Console.ReadLine();
                            addressBooksCollection.SearchAllAddressBooksByCity(city);
                            break;
                        default:
                            Console.WriteLine("Wrong Choice\n");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Wrong Option Entered !!");
                    Console.WriteLine("Continuing with "+addressBooksCollection.Name+" address book");
                    break;
            }
        }
    }
}
