using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api;
using MyToDo.Shared.Dtos;
using System.Security.Principal;
using MyToDo.Shared.Extensions;

namespace MyToDo.Api.Service
{
    public class MT2_LoginService : MT2_ILoginService
    {
        private readonly UOW_IUnitOfWork work;
        private readonly IMapper mapper;

        public MT2_LoginService(UOW_IUnitOfWork work,IMapper mapper) 
        {
            this.work = work;
            this.mapper = mapper;
        }
        public async Task<MT2_ServiceApiResponse> MT2_LoginAsync(string Account, string Password)
        {
            try 
            {
                Password = Password.MT3_GetMD5();

                var model = await work.GetRepository<MT2_User>().GetFirstOrDefaultAsync(predicate:
                    x => (x.Account.Equals(Account)) &&
                    (x.Password.Equals(Password))
                    );
                if(model == null) { return new MT2_ServiceApiResponse("账号或密码错误，请重试"); }
                else
                {
                    return new MT2_ServiceApiResponse(true, model);
                }
            }
            catch (Exception ex) {
                return new MT2_ServiceApiResponse(false,"登录失败！");
            }
        }

        public async Task<MT2_ServiceApiResponse> MT2_RegisterAsync(MT3_UserDto user)
        {
            try
            {
                var model = mapper.Map<MT2_User>(user);
                var repository = work.GetRepository<MT2_User>();

                var userModel = await repository.GetFirstOrDefaultAsync(predicate:
                    x => x.Account.Equals(model.Account)
                    );


                if (userModel != null) 
                {
                    return new MT2_ServiceApiResponse("当前账号已存在");
                }
                else
                {
                    model.CreateDate = DateTime.Now;
                    model.Password = model.Password.MT3_GetMD5();
                    await repository.InsertAsync(model);
                }

                if (await work.SaveChangesAsync() > 0)
                {
                    return new MT2_ServiceApiResponse(true, model);
                }
                else 
                { 
                    return new MT2_ServiceApiResponse("注册失败"); 
                }

                

            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse("注册账号失败"+ex.ToString());
            }
        }
    }
}
