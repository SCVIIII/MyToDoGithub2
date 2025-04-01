using Microsoft.EntityFrameworkCore;

namespace MyToDo.Api.Context
{
    /// <summary>
    /// 记录操作的数据库的表名
    /// 数据库名存放于appsettings.json
    /// </summary>
    public class MT2_MyToDoContext:DbContext
    {
        public MT2_MyToDoContext(DbContextOptions<MT2_MyToDoContext> options):base(options) 
        { 


        
        }
        
        public DbSet<MT2_ToDo> ToDo {  get; set; }
        public DbSet<MT2_User> User { get; set; }
        public DbSet<MT2_Memo> Memo { get; set; }

    }
}
