using System;
using System.Collections.Generic;
using System.Text;
using AddressBook.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AddressBook.DBoperations
{
    public class JsonServerOperation : IDataSourceOperation
    {
        public DictToListMapping ReadFromDataSource()
        {
            DictToListMapping dictToList = new DictToListMapping();
            
            dictToList.AddressBookName = JsonConvert.DeserializeObject<List<string>>(GetDataList("AddressBookName").Content);
            dictToList.ContactName = JsonConvert.DeserializeObject<List<string>>(GetDataList("ContactName").Content);
            dictToList.FirstName = JsonConvert.DeserializeObject<List<string>>(GetDataList("FirstName").Content);
            dictToList.LastName= JsonConvert.DeserializeObject<List<string>>(GetDataList("LastName").Content);
            dictToList.City = JsonConvert.DeserializeObject<List<string>>(GetDataList("City").Content);
            dictToList.State = JsonConvert.DeserializeObject<List<string>>(GetDataList("State").Content);
            dictToList.ZipCode = JsonConvert.DeserializeObject<List<string>>(GetDataList("ZipCode").Content);
            dictToList.PhoneNumber = JsonConvert.DeserializeObject<List<string>>(GetDataList("PhoneNumber").Content);
            dictToList.Email = JsonConvert.DeserializeObject<List<string>>(GetDataList("Email").Content);
            dictToList.DateAdded = JsonConvert.DeserializeObject<List<DateTime>>(GetDataList("DateAdded").Content);

            return dictToList;
        }

        public bool WriteToDataSource(DictToListMapping dictToList)
        {
            RestClient client = new RestClient("http://localhost:3000");
            JObject jObjectbody = new JObject();
            IRestResponse response = new RestResponse();
            RestRequest request;
            for (int i=0; i<dictToList.AddressBookName.Count; i++)
            {
                jObjectbody.Add("AddressBookName", dictToList.AddressBookName[i]);
                jObjectbody.Add("ContactName", dictToList.ContactName[i]);
                jObjectbody.Add("FirstName", dictToList.FirstName[i]);
                jObjectbody.Add("LastName", dictToList.LastName[i]);
                jObjectbody.Add("City", dictToList.City[i]);
                jObjectbody.Add("State", dictToList.State[i]);
                jObjectbody.Add("Zip", dictToList.ZipCode[i]);
                jObjectbody.Add("PhoneNumber", dictToList.PhoneNumber[i]);
                jObjectbody.Add("Email", dictToList.Email[i]);
                jObjectbody.Add("DateAdded", dictToList.Email[i]);

                request = new RestRequest("/AddressBook",  Method.POST);
                request.AddParameter("application/Json",jObjectbody, ParameterType.RequestBody);

                response = client.Execute(request);
                
            }
            
            return (response.StatusCode == System.Net.HttpStatusCode.Created);
                
        }

        
        private IRestResponse GetDataList(string property)
        {
            RestClient client = new RestClient("http://localhost:3000");
            RestRequest request = new RestRequest("/"+property, Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}
