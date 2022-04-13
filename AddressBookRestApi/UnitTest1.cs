using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace AddressBookRestApi
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            //Initialize the base URL to execute requests made by the instance
            client = new RestClient("http://localhost:3000");
        }
        private RestResponse GetContactList()
        {
            //Arrange
            //Initialize the request object with proper method and URL
            RestRequest request = new RestRequest("/Contacts", Method.Get);
            //Act
            // Execute the request
            RestSharp.RestResponse response = client.ExecuteAsync(request).Result;
            return response;
        }

        /* UC22:- Ability to Read Entries of Address Book from JSONServer.
                  - Use RESTSharp for REST Api Calls from MSTest Test Code.
        */
        [TestMethod]
        public void ReadEntriesFromJsonServer()
        {
            RestResponse response = GetContactList();
            // Check if the status code of response equals the default code for the method requested
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            // Convert the response object to list of employees
            List<Contact> employeeList = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            Assert.AreEqual(2, employeeList.Count);
            foreach (Contact c in employeeList)
            {
                Console.WriteLine($"Id: {c.Id}\tFullName: {c.FirstName} {c.LastName}\tPhoneNo: {c.PhoneNumber}\tAddress: {c.Address}\tCity: {c.City}\tState: {c.State}\tZip: {c.Zip}\tEmail: {c.Email}");
            }
        }
    }
}
