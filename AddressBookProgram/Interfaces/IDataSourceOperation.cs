using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Interfaces
{
    public interface IDataSourceOperation
    {
        public DictToListMapping ReadFromDataSource();
        
        public bool WriteToDataSource(DictToListMapping dictToList);
    }
}
