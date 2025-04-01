using MyToDo.Common;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class MT1_ToDoService : MT1_BaseService<MT3_ToDoDto>, MT1_IToDoService
    {
        private readonly MT1_HttpRestClient client;
        private static readonly string serviceName = "MT2_ToDo";
        public MT1_ToDoService(MT1_HttpRestClient client) : base(client, serviceName)
        {
            this.client = client;
        }

        public async Task<MT3_ApiResponse<MT3_PagedList<MT3_ToDoDto>>> MT2_ILogin_GetAllFilterAsync(MT3_ToDoParameter parameter)
        {
            //$"api/{serviceName}/
            MT1_BaseRequest request = new MT1_BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{serviceName}/GetAll?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"{(!string.IsNullOrWhiteSpace(parameter.Search) ? $"&Search={parameter.Search}" : "")}" +
                $"{(!string.IsNullOrWhiteSpace(parameter.Status.ToString()) ? $"&Status={parameter.Status}" : "")}";
            return await client.ExecuteAsync<MT3_PagedList<MT3_ToDoDto>>(request);
        }

        public async Task<MT3_ApiResponse<MT3_SummaryDto>> SummaryAsync()
        {
            MT1_BaseRequest request = new MT1_BaseRequest();
            request.Route = $"api/{serviceName}/Summary";
            return await client.ExecuteAsync<MT3_SummaryDto>(request);
        }
    }
}
