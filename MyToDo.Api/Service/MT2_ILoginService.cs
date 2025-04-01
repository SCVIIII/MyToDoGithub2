using MyToDo.Shared;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Service
{
    public interface MT2_ILoginService
    {
        Task<MT2_ServiceApiResponse> MT2_LoginAsync(string Account,string Password);

        Task<MT2_ServiceApiResponse> MT2_RegisterAsync(MT3_UserDto dto);

    }
}
