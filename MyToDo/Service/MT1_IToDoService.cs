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
    public interface MT1_IToDoService:IBaseService<MT3_ToDoDto>
    {
        Task<MT3_ApiResponse<MT3_PagedList<MT3_ToDoDto>>> MT2_ILogin_GetAllFilterAsync(MT3_ToDoParameter parameter);

        Task<MT3_ApiResponse<MT3_SummaryDto>> SummaryAsync();
    }
}
