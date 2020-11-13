using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook;
using AddressBook.DBoperations;
using System.Collections.Generic;
using System;
using System.Data;

namespace TestAddressBookProgram
{
    [TestClass]
    public class TestLinqOperations
    {
        [TestMethod]
        public void ReadFromSqlServer_ShouldReturnAllFiles_AsAddressBookObject()
        {
            //Arrange
            AddressBooks addressBooks = new AddressBooks();
            //Act
            SqlServerOperation.ReadFromSqlServer(ref addressBooks);
            //Assert
            Assert.AreEqual(2, addressBooks._multiAddressBooks["General"].AddressBook.Count);
        }

        [TestMethod]
        public void ContactsAddedBetweenDateRange_ReturnTwo_ForGivenDateRange()
        {
            //Act
            List<DataRow> data = SqlServerOperation.ContactsAddedBetweenDateRange(new DateTime(2020, 11, 01), DateTime.Today);
            //Assert
            Assert.AreEqual(2, data.Count);
        }

        [TestMethod]
        public void CountByCity_ReturnOne_ForGivenCity()
        {
            //Arrange
            string city = "Ranchi";
            //Act
            Dictionary<string, int> countByCity = SqlServerOperation.CountByCity();
            //Assert
            Assert.AreEqual(1, countByCity[city]);
        }

        [TestMethod]
        public void CountByState_ReturnOne_ForGivenState()
        {
            //Arrange
            string state = "Ods"; 
            //Act
            Dictionary<string, int> countByState = SqlServerOperation.CountByState();
            //Assert
            Assert.AreEqual(1, countByState[state]);
        }

    }
}
