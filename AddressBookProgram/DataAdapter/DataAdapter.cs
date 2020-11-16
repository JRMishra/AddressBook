using System;
using System.Collections.Generic;
using System.Text;
using AddressBook.Interfaces;
using AddressBook.DBoperations;
using AddressBook.mapping;
using AddressBook.MockClasses;


namespace AddressBook.DataAdapter
{ 
    public class AddressBookDataAdapter
    {
        public enum OperationType
        {
            XMl,
            CSV,
            JSON,
            JsonServer,
            SqlServer,
            Mock
        }

        public bool Reader(OperationType type, ref AddressBooks addressBooks)
        {
            LogDetails logDetails = new LogDetails();
            IDataSourceOperation dataSourceOperation;
            if (type == OperationType.Mock)
                dataSourceOperation = new MockDataSourceOperation();
            else
                dataSourceOperation = new JsonOperation();
            try
            {
                addressBooks = DictToListMapping.ListToDictionary(Reader(dataSourceOperation));
            }
            catch(Exception e)
            {
                logDetails.LogDebug("IO Error in Reading operation");
                logDetails.LogError(e.Message);
                return false;
            }
            return true;
        }

        private DictToListMapping Reader(IDataSourceOperation dataSourceOperation)
        {
            return dataSourceOperation.ReadFromDataSource();
        }
    }
}
