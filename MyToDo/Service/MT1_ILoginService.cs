using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface MT1_ILoginService
    {
        
        Task<MT3_ApiResponse<MT3_UserDto>> MT1_LoginAsync(string Account, string Password);

        Task<MT3_ApiResponse<MT3_UserDto>> MT1_RegisterAsync(MT3_UserDto dto);

    }
}
 