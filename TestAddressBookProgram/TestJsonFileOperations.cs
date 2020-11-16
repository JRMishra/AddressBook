using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook;
using AddressBook.DBoperations;

namespace TestAddressBookProgram
{
    [TestClass]
    public class TestJsonFileOperations
    {
        [TestMethod]
        public void ReadFromDataSource_ShouldPopulateAddressBookObject_WithDataFromJsonFile()
        {
            //Arrange
            DictToListMapping dictToList = new DictToListMapping();
            JsonOperation jsonOperation = new JsonOperation();
            //Act
            dictToList = jsonOperation.ReadFromDataSource();
            //Assert
            Assert.AreEqual("General", dictToList.AddressBookName[0]);
            Assert.AreEqual("Jyoti RanjanMishra", dictToList.ContactName[0]);
        }
    }
}
