using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api;
using MyToDo.Api.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using MyToDo.Shared;


namespace MyToDo.Api.Controllers
{

    /// <summary>
    /// 待办事项控制器
    /// </summary>
    [ApiController]
    [Route ("api/[controller]/[action]")]
    public class MT2_ToDoController:Controller
    {

        private readonly MyToDo.Api.Service.MT2_IToDoService service;
        public MT2_ToDoController(MyToDo.Api.Service.MT2_IToDoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<MyToDo.Api.Service.MT2_ServiceApiResponse> Get(int id) => await service.MT2_GetSingleAsync(id);
        [HttpGet]
        public async Task<MT2_ServiceApiResponse> GetAll([FromQuery] MT3_ToDoParameter query) => await service.MT2_GetAllAsync(query);
        [HttpGet]
        public async Task<MyToDo.Api.Service.MT2_ServiceApiResponse> Summary() => await service.Summary();
        [HttpPost]
        public async Task<MT2_ServiceApiResponse> Add([FromBody]MT3_ToDoDto model) => await service.MT2_AddAsync(model);
        [HttpPost]
        public async Task<MT2_ServiceApiResponse> Update(MT3_ToDoDto model) => await service.MT2_UpdateAsync(model);
        [HttpDelete]
        public async Task<MT2_ServiceApiResponse> Delete(int id) => await service.MT2_DeleteAsync(id);
        


    }
}
