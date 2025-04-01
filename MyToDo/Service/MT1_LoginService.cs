using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class MT1_LoginService : MT1_ILoginService
    {
        private readonly MT1_HttpRestClient client;
        private readonly string serviceName = "MT2_Login";

        public MT1_LoginService(MT1_HttpRestClient client)
        {
            this.client = client;
        }


        public async Task<MT3_ApiResponse<MT3_UserDto>> MT1_LoginAsync(string Account, string Password)
        {
            MT1_BaseRequest request = new MT1_BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Login";
            request.Parameter = new Dictionary<string, string>
            {
                { "Account", Account },
                { "Password", Password }
            };
            return await client.ExecuteAsync<MT3_UserDto>(request);
        }

        public async Task<MT3_ApiResponse<MT3_UserDto>> MT1_RegisterAsync(MT3_UserDto dto)
        {
            MT1_BaseRequest request = new MT1_BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Register";
            request.Parameter = dto;
            return await client.ExecuteAsync<MT3_UserDto>(request);
        }
        
    }
}
