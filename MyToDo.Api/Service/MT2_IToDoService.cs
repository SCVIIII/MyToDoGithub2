using MyToDo.Api.Context;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Service
{
    public interface MT2_IToDoService :MT2_ApiIBaseService<MT3_ToDoDto>
    {

        Task<MT2_ServiceApiResponse> MT2_GetAllAsync(MT3_ToDoParameter query);

        Task<MT2_ServiceApiResponse> Summary();
    }
}
