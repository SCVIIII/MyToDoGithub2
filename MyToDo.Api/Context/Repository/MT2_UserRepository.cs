using MyToDo.Api;

namespace MyToDo.Api.Context.Repository
{
    
    public class UserRepository : UOW_Repository<MT2_User>, UOW_IRepository<MT2_User>
    {
        public UserRepository(MT2_MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}
