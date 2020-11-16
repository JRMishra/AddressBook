using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook;
using AddressBook.DBoperations;

namespace TestAddressBookProgram
{
    [TestClass]
    public class TestJsonServerOperation
    {
        [TestMethod]
        public void ReadFromDataSource_ShouldPopulateAddressBookObject_WithDataFromJsonServerFile()
        {
            //Arrange 
            DictToListMapping dictToList = new DictToListMapping();
            JsonServerOperation jsonServer = new JsonServerOperation();
            //Act
            dictToList = jsonServer.ReadFromDataSource();
            //Assert
            Assert.AreEqual("General", dictToList.AddressBookName[0]);
            Assert.AreEqual("Jyoti RanjanMishra", dictToList.ContactName[0]);
            Assert.AreEqual("Jyoti Ranjan", dictToList.FirstName[0]);
            Assert.AreEqual("Mishra", dictToList.LastName[0]);
            Assert.AreEqual("Baripada", dictToList.City[0]);
            Assert.AreEqual("Odisha", dictToList.State[0]);
            Assert.AreEqual("970173", dictToList.ZipCode[0]);
            Assert.AreEqual("jrm@mymail.com", dictToList.Email[0]);
            Assert.AreEqual(new System.DateTime(2020,11,13), dictToList.DateAdded[0].Date);
        }

        [TestMethod]
        public void WriteToDataSource_ShouldWriteToJsonServer_WithSpecifiedData()
        {
            //Arrange 
            DictToListMapping dictToList = new DictToListMapping();
            dictToList.AddressBookName.Add("Testing");
            dictToList.ContactName.Add("Testing");
            dictToList.FirstName.Add("Testing");
            dictToList.LastName.Add("Testing");
            dictToList.City.Add("Testing");
            dictToList.State.Add("Testing");
            dictToList.ZipCode.Add("Testing");
            dictToList.PhoneNumber.Add("Testing");
            dictToList.Email.Add("Testing");
            dictToList.DateAdded.Add(new System.DateTime(2020,01,01));
            JsonServerOperation jsonServer = new JsonServerOperation();
            //Act
            bool status = jsonServer.WriteToDataSource(dictToList);
            //Assert
            Assert.IsTrue(status);
        }

    }
}
