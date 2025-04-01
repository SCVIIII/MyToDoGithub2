using MyToDo.Shared;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MyToDo.Api.Service
{
    public interface MT2_ApiIBaseService<T>
    {
        Task<MT2_ServiceApiResponse> MT2_GetAllAsync(MT3_QueryParameter query);
        Task<MT2_ServiceApiResponse> MT2_GetSingleAsync(int id);
        Task<MT2_ServiceApiResponse> MT2_AddAsync(T model);
        Task<MT2_ServiceApiResponse> MT2_UpdateAsync(T model);
        Task<MT2_ServiceApiResponse> MT2_DeleteAsync(int id);

    }
}
