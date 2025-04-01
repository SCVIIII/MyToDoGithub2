using Microsoft.EntityFrameworkCore;
using MyToDo.Api;

namespace MyToDo.Api.Context.Repository
{

    public class ToDoRepository : UOW_Repository<MT2_ToDo>, UOW_IRepository<MT2_ToDo>
    {
        public ToDoRepository(MT2_MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}


