using RestApiAutomation.Request;
using RestApiAutomation.Utility;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Net;
using System.Reflection;
using System;

namespace RestApiAutomation.SpecFlow.StepDefinitions
{
    public class TestRestApi
    {
        private readonly JsonReader _jsonReader = new JsonReader();
        WebServiceRequests _webServiceRequests = new WebServiceRequests();
        //ExtentReporting _extentReporting = new ExtentReporting();
        MethodBase method;

        [Test]
        public void ExecuteGetRequest()
        {
            var getRequestURL = _jsonReader.ReadJson("WSData.json", "GET"); /*"getRequestURL";*/
            var irestResponse = _webServiceRequests.GetRequest(getRequestURL);
            var response = irestResponse.Content.ToString();
            HttpStatusCode statusCode = irestResponse.StatusCode;
            int numericStatusCode = (int)statusCode;
            method = MethodBase.GetCurrentMethod();

            ExtentReporting.SetupExtentReport("Webservice Test Report", "Webservice Request");
            ExtentReporting.CreateTest(method.Name);
            dynamic results = JObject.Parse(response);
            dynamic data = JObject.Parse(_jsonReader.ReadJson("GetResults.json"));
            var responseCode = _jsonReader.ReadJson("WSData.json", "GET_RESPONSE_CODE");

            try
            {
                //For the 1st object
                //Assert.AreEqual("expectedValue", "actualValue");
                Assert.AreEqual((string)data.data[0].first_name, (string)results.data[0].first_name);

                /*
                Example:
                Assert.AreEqual((string)data.data[0].first_name, (string)results.data[0].first_name);
                1. Need to write assertions depending on the number of objects and number of fields returned from each objects.                  
                */

                //Verify the response code
                //Assert.AreEqual("expected_Response_status", responseStatus);

                ///*Example:
                Assert.AreEqual(responseCode, numericStatusCode.ToString());

                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Pass, method.Name.ToString() + " TestPassed \n" + "\n API Resposne:" + "\n" + irestResponse.Content);
            }
            catch (AssertionException e)
            {
                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Error, e.Message);
                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Fail, method.Name.ToString() + " TestFailed: " + e.Message);
            }
            ExtentReporting.FlushReport();
        }
    }
}
