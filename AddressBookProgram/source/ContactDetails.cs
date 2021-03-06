﻿namespace AddressBook
{
    using System;
    using System.Data;

    public class ContactDetails
    {
        string _firstName;
        string _lastName;
        string _city;
        string _state;
        string _zip;
        string _phNo;
        string _email;
        DateTime _dateAdded;

        public ContactDetails()
        {
            this._firstName = "";
            this._lastName = "";
            this._city = "-";
            this._state = "-";
            this._zip = "-";
            this._phNo = "";
            this._email = "";
        }

        public ContactDetails(string FirstName, string LastName, string City, string State, string Zip,string PhoneNumber,string Email)
        {
            this._firstName = FirstName;
            this._lastName = LastName;
            this._city = City;
            this._state = State;
            this._zip = Zip;
            this._phNo = PhoneNumber;
            this._email = Email;
            this._dateAdded = DateTime.Today;
        }

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string PhoneNumber { get => _phNo; set => _phNo = value; }
        public string Email { get => _email; set => _email = value; }
        public string City { get=>_city; set=>_city=value; }
        public string State { get => _state; set => _state = value; }
        public string Zip { get => _zip; set => _zip = value; }
        public DateTime DateAdded { get => _dateAdded; set => _dateAdded = value; }

        /// <summary>
        /// </summary>
        /// <returns>All details of a contact as single string</returns>
        public string Display()
        {
            return "\nName : " + this.FirstName + " " + this.LastName + "\nAddress : " + this.City + "," + this.State + "," + this.Zip +
                "\nPhone : " + this.PhoneNumber + "\nEmail Id : " + this.Email;
        }

        /// <summary>
        /// Overridden method to return full name of a contact
        /// </summary>
        /// <returns>firstname+" "+lastname</returns>
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
