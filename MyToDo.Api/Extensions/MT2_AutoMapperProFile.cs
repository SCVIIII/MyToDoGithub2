using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Controllers;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Extensions
{
    public class MT2_AutoMapperProFile 
    {

        private MapperConfigurationExpression mapperConfigurationExpression;
        public MapperConfigurationExpression MapperCE
        {
            get { return  mapperConfigurationExpression; }
            set { mapperConfigurationExpression = value; }

        }
        public MT2_AutoMapperProFile()
        {
            MapperCE =new MapperConfigurationExpression();
            MapperCE.CreateMap<MT2_ToDo, MT3_ToDoDto>().ReverseMap();
            MapperCE.CreateMap<MT2_Memo, MT3_MemoDto>().ReverseMap();
            MapperCE.CreateMap<MT2_User, MT3_UserDto>().ReverseMap();

        }
    }
}
