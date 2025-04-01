using MyToDo.Api;
using MyToDo.Shared;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Parameters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MT3_QueryParameter = MyToDo.Shared.Parameters.MT3_QueryParameter;

namespace MyToDo.Service
{
    public class MT1_BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly MT1_HttpRestClient client;
        private readonly string serviceName;

        public MT1_BaseService(MT1_HttpRestClient client,string serviceName) 
        {
            this.client = client;
            this.serviceName = serviceName;
        }
        public async Task<MT3_ApiResponse<TEntity>> MT1_AddAsync(TEntity entity)
        {
            MT1_BaseRequest request = new MT1_BaseRequest()
            {
                Method = RestSharp.Method.Post,
                Route = $"api/{serviceName}/Add",
                Parameter = entity
            };
            var result = await client.ExecuteAsync<TEntity>(request);
            return result;
        }

        public async Task<MT3_ApiResponse> MT1_DeleteAsync(int id)
        {
            MT1_BaseRequest request = new MT1_BaseRequest()
            {
                Method = RestSharp.Method.Delete,
                Route = $"api/{serviceName}/Delete?id={id}",
            };
            var result = await client.ExecuteAsync(request);
            return result;
        }

        public async Task<MT3_ApiResponse<MT3_PagedList<TEntity>>> MT1_GetAllAsync(MT3_QueryParameter queryParameter)
        {
            
            MT1_BaseRequest request = new MT1_BaseRequest();
            request.Method = RestSharp.Method.Get;

            if(string.IsNullOrWhiteSpace(queryParameter.Search))
            {
                request.Route = $"api/{serviceName}/GetAll?pageIndex={queryParameter.PageIndex}" +
                $"&pageSize={queryParameter.PageSize}"
                ;
            }
            else
            {
                request.Route = $"api/{serviceName}/GetAll?pageIndex={queryParameter.PageIndex}" +
                $"&pageSize={queryParameter.PageSize}"
                + $"&search={queryParameter.Search}"
                ;
            }
            
            //+$"&search={queryParameter.Search}"
            var result = await client.ExecuteAsync<MT3_PagedList<TEntity>>(request);
            return result;
        }

        public async Task<MT3_ApiResponse<TEntity>> MT1_GetFirstOfDefaultAsync(int id)
        {
            MT1_BaseRequest request = new MT1_BaseRequest()
            {
                Method = RestSharp.Method.Get,
                Route = $"api/{serviceName}/Get?id={id}",
            };
            var result = await client.ExecuteAsync<TEntity>(request);
            return result;
        }

        public async Task<MT3_ApiResponse<TEntity>> MT1_UpdateAsync(TEntity entity)
        {
            MT1_BaseRequest request = new MT1_BaseRequest()
            {
                Method = RestSharp.Method.Post,
                Route = $"api/{serviceName}/Update",
                Parameter = entity
            };
            var result = await client.ExecuteAsync<TEntity>(request);
            return result;
        }
    }
}
