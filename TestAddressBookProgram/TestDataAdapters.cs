using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook;
using AddressBook.DataAdapter;

namespace TestAddressBookProgram
{
    [TestClass]
    public class TestDataAdapters
    {
        [TestMethod]
        public void DataAdapterReader_WhenGivenMockTypeShouldReturn_AdressBookWithMockValues()
        {
            //Arrange
            AddressBooks addressBooks = new AddressBooks();
            AddressBookDataAdapter adapter = new AddressBookDataAdapter();
            //Act
            adapter.Reader(AddressBookDataAdapter.OperationType.Mock, ref addressBooks);
            //Assert
            Assert.AreEqual("TestFirstName", addressBooks._multiAddressBooks["TestAddressBook"].AddressBook["TestContactName"].FirstName);
            Assert.AreEqual("TestLastName", addressBooks._multiAddressBooks["TestAddressBook"].AddressBook["TestContactName"].LastName);
        }
        
        [TestMethod]
        public void DataAdapterReader_WhenGivenJsonTypeShouldReturn_AdressBookListWithValuesInJsonFile()
        {
            //Arrange
            AddressBooks addressBooks = new AddressBooks();
            AddressBookDataAdapter adapter = new AddressBookDataAdapter();
            //Act
            adapter.Reader(AddressBookDataAdapter.OperationType.JSON, ref addressBooks);
            //Assert
            Assert.AreEqual("Jyoti Ranjan", addressBooks._multiAddressBooks["General"].AddressBook["Jyoti RanjanMishra"].FirstName);
            Assert.AreEqual("Mishra", addressBooks._multiAddressBooks["General"].AddressBook["Jyoti RanjanMishra"].LastName);
        }
    }
}
