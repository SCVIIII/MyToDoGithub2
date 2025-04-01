using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api;
using MyToDo.Api.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;


namespace MyToDo.Api.Controllers
{

    /// <summary>
    /// 账户控制器
    /// </summary>
    [ApiController]
    [Route ("api/[controller]/[action]")]
    public class MT2_LoginController : Controller
    {

        private readonly MT2_ILoginService service;
        public MT2_LoginController(MT2_ILoginService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<MT2_ServiceApiResponse> Login(string Account,string Password) => await service.MT2_LoginAsync( Account,Password);
        //public async Task<MT2_ServiceApiResponse> Login([FromBody] MT3_UserDto model) => await service.MT2_LoginAsync(model.Account, model.Password);
        [HttpPost]
        public async Task<MT2_ServiceApiResponse> Register([FromBody] MT3_UserDto model) => await service.MT2_RegisterAsync(model); 

    }
}
