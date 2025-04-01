using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api;
using MyToDo.Api.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using MyToDo.Shared;


namespace MyToDo.Api.Controllers
{

    /// <summary>
    /// 备忘录控制器
    /// </summary>
    [ApiController]
    [Route ("api/[controller]/[action]")]
    public class MT2_MemoController : Controller
    {

        private readonly MT2_IMemoService service;
        public MT2_MemoController(MT2_IMemoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<MT2_ServiceApiResponse> Get(int id) => await service.MT2_GetSingleAsync(id);
        [HttpGet]
        public async Task<MT2_ServiceApiResponse> GetAll([FromQuery] MT3_QueryParameter query) => await service.MT2_GetAllAsync(query);
        [HttpPost]
        public async Task<MT2_ServiceApiResponse> Add([FromBody] MT3_MemoDto model) => await service.MT2_AddAsync(model); 
        [HttpPost]
        public async Task<MT2_ServiceApiResponse> Update([FromBody] MT3_MemoDto model) => await service.MT2_UpdateAsync(model);
        [HttpDelete]
        public async Task<MT2_ServiceApiResponse> Delete(int id) => await service.MT2_DeleteAsync(id);
        


    }
}
