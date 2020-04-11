using RestSharp;

namespace RestApiAutomation.Request
{
    class WebServiceRequests
    {
        public IRestResponse GetRequest(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
