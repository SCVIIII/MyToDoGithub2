using RestSharp;
using Newtonsoft.Json;
using MyToDo.Shared;
using MyToDo.Shared.Contact;


namespace MyToDo.Service
{
    public class MT1_HttpRestClient
    {
        private readonly string apiUrl;

        protected  RestClient  client;

        public MT1_HttpRestClient(string apiUrl) 
        {
            this.apiUrl = apiUrl;
            //client = new RestClient();
        }

        public async Task<MT3_ApiResponse> ExecuteAsync(MT1_BaseRequest baseRequest)
        {
            var options = new RestClientOptions(apiUrl);
            client = new RestClient(options);
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);

            request.AddHeader("Content-Type", baseRequest.ContentType);


            if (baseRequest.Parameter != null)
            {
                if (baseRequest.Parameter is Dictionary<string, string> parameters)
                {
                    foreach (var param in parameters)
                    {
                        request.AddQueryParameter(param.Key, param.Value);
                    }
                }

                else 
                {
                    request.AddJsonBody(baseRequest.Parameter);
                }
            }
                
            //request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var returnvalue = JsonConvert.DeserializeObject<MT3_ApiResponse>(response.Content);
                return returnvalue;
            }

            else
                return new MT3_ApiResponse()
                {
                    Status = false,
                    Message = response.ErrorMessage
                };
        }
        

        public async Task<MT3_ApiResponse<T>> ExecuteAsync<T>(MT1_BaseRequest baseRequest)
        {

            var options = new RestClientOptions(apiUrl);
            var client = new RestClient(options);
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);

            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter != null)
            {
                if (baseRequest.Parameter is Dictionary<string, string> parameters)
                {
                    foreach (var param in parameters)
                    {
                        request.AddQueryParameter(param.Key, param.Value);
                    }
                }

                else
                {
                    request.AddJsonBody(baseRequest.Parameter);
                }
                //request.AddJsonBody(JsonConvert.SerializeObject(baseRequest.Parameter));
                //var testjs= (JsonConvert.SerializeObject(baseRequest.Parameter));
            }

            RestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var returnvalue = JsonConvert.DeserializeObject<MT3_ApiResponse<T>>(response.Content);
                return returnvalue;
            }

            else
                return new MT3_ApiResponse<T>()
                {
                    Status = false,
                    Message = response.ErrorMessage
                };
        }
    }
}
