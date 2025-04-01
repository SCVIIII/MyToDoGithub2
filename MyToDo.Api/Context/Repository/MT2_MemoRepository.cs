using MyToDo.Api;

namespace MyToDo.Api.Context.Repository
{
    
    public class MemoRepository : UOW_Repository<MT2_Memo>, UOW_IRepository<MT2_Memo>
    {
        public MemoRepository(MT2_MyToDoContext dbContext) : base(dbContext)
        {

        }
    }
}
