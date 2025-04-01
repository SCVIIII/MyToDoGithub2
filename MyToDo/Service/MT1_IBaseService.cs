using MyToDo.Shared;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<MT3_ApiResponse<TEntity>> MT1_AddAsync(TEntity entity);
        Task<MT3_ApiResponse<TEntity>> MT1_UpdateAsync(TEntity entity);
        Task<MT3_ApiResponse> MT1_DeleteAsync(int id);
        Task<MT3_ApiResponse<TEntity>> MT1_GetFirstOfDefaultAsync(int id);
        Task<MT3_ApiResponse<MT3_PagedList<TEntity>>> MT1_GetAllAsync(MT3_QueryParameter queryParameter);


    }

}
