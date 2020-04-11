using System;
using TechTalk.SpecFlow;
using RestApiAutomation.Request;
using RestApiAutomation.Utility;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace RestApiAutomation.SpecFlow.StepDefinitions
{
    [Binding]
    public class GetRequestFeatureSteps
    {
        private JsonReader _jsonReader = new JsonReader();
        private IRestResponse _iRestResponse;
        private string _responseStatus;
        private string _apiUrl;
        WebServiceRequests _allWebServiceRequests = new WebServiceRequests();

        private ScenarioContext _scenarioContext;

        public GetRequestFeatureSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have the API URL for GET request")]
        public void GivenIHaveTheAPIURLForGETRequest()
        {
            ExtentReporting.SetupExtentReport("Webservice Test Report", "Webservice Request");
            ExtentReporting.CreateTest(_scenarioContext.ScenarioInfo.Title);
            _apiUrl = _jsonReader.ReadJson("WSData.json", "GET");

            if (_apiUrl != "")
            {
                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Pass, _scenarioContext.StepContext.StepInfo.StepDefinitionType + " " + _scenarioContext.StepContext.StepInfo.Text + " Step Passed");
            }

            else
            {
                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Fail, _scenarioContext.StepContext.StepInfo.StepDefinitionType + " " + _scenarioContext.StepContext.StepInfo.Text + " Step Failed: " + "There is something worng with the URL. Please check the URL and try again.");
                ExtentReporting.FlushReport();
                Assert.Fail(_scenarioContext.StepContext.StepInfo + " Step Failed");
            }
        }
        
        [When(@"I call the URL")]
        public void WhenICallTheURL()
        {
            _iRestResponse = _allWebServiceRequests.GetRequest(_apiUrl);

            if (_iRestResponse.ResponseStatus.ToString() != "Error")
            {
                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Pass, _scenarioContext.StepContext.StepInfo.StepDefinitionType + " " + _scenarioContext.StepContext.StepInfo.Text + " Step Passed");
            }

            else
            {
                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Fail, _scenarioContext.StepContext.StepInfo.StepDefinitionType + " " + _scenarioContext.StepContext.StepInfo.Text + " Step Failed: " + _iRestResponse.ErrorMessage.ToString());
                ExtentReporting.FlushReport();
                Assert.Fail(_scenarioContext.StepContext.StepInfo + " Step Failed");
            }
        }
        
        [Then(@"The details are returned")]
        public void ThenTheDetailsAreReturned()
        {
            dynamic data = JObject.Parse(_jsonReader.ReadJson("GetResults.json"));
            dynamic results;
            if (_iRestResponse.ResponseStatus.ToString() != "Error")
            {

                results = JObject.Parse(_iRestResponse.Content.ToString());
                HttpStatusCode statusCode = _iRestResponse.StatusCode;
                int numericStatusCode = (int)statusCode;
                _responseStatus = numericStatusCode.ToString();

            }
            else
            {
                results = null;
                _responseStatus = null;
            }

            var responseCode = _jsonReader.ReadJson("WSData.json", "GET_RESPONSE_CODE");

            try
            {
                if (results != null)
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
                    Assert.AreEqual(responseCode, _responseStatus);

                }
                else
                {
                    // If the created detals are not returned
                    //Assert.AreEqual("Expected_Response_Status", responseStatus);

                    ///*Example:
                    Assert.AreEqual(responseCode, _responseStatus);

                }

                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Pass, _scenarioContext.StepContext.StepInfo.StepDefinitionType + " " + _scenarioContext.StepContext.StepInfo.Text + " Step Passed");
                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Pass, _scenarioContext.ScenarioInfo.Title.ToString() + " TestPassed");
            }
            catch (AssertionException e)
            {
                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Fail, _scenarioContext.StepContext.StepInfo.StepDefinitionType + " " + _scenarioContext.StepContext.StepInfo.Text + " Step Failed");
                ExtentReporting.LogReportStatement(AventStack.ExtentReports.Status.Fail, _scenarioContext.ScenarioInfo.Title.ToString() + " TestFailed: " + e.Message);
            }

            ExtentReporting.FlushReport();
        }
    }
}
