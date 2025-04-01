using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Service
{
    /// <summary>
    /// 备忘录的实现
    /// </summary>
    public class MT2_MemoService : MT2_IMemoService
    {
        private readonly UOW_IUnitOfWork work;
        private readonly IMapper Mapper;
        
        public MT2_MemoService(UOW_IUnitOfWork work,IMapper mapper)
        {
            this.work = work;
            this.Mapper = mapper;
        }

        public async Task<MT2_ServiceApiResponse> MT2_AddAsync(MT3_MemoDto model)
        {
            try 
            {
                var memo = Mapper.Map<MT2_Memo>(model);
                await work.GetRepository<MT2_Memo>().InsertAsync(memo);
                if (await work.SaveChangesAsync() > 0)
                {
                    return new MT2_ServiceApiResponse(true, memo);
                }

                return new MT2_ServiceApiResponse("添加数据失败");

            }
            catch (Exception ex) 
            {
                return new MT2_ServiceApiResponse(ex.Message);
            }


        }

        public async Task<MT2_ServiceApiResponse> MT2_DeleteAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<MT2_Memo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                
                repository.Delete(todo);

                if (await work.SaveChangesAsync() > 0)
                {
                    return new MT2_ServiceApiResponse(true,"");
                }

                return new MT2_ServiceApiResponse("删除数据失败");

            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse(ex.Message);
            }
        }

        public async Task<MT2_ServiceApiResponse> MT2_GetAllAsync(MT3_QueryParameter query)
        {
            try
            {
                var repository = work.GetRepository<MT2_Memo>();
                var todos = await repository.GetPagedListAsync(predicate:
                    x => string.IsNullOrWhiteSpace(query.Search) ? true : x.Title.Contains(query.Search),
                    pageSize: query.PageSize,
                    pageIndex: query.PageIndex,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate)

                    );

                return new MT2_ServiceApiResponse(true, todos);

            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse(ex.Message);
            }
        }

        public async Task<MT2_ServiceApiResponse> MT2_GetSingleAsync(int id)
        {
            var repository = work.GetRepository<MT2_Memo>();
            var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return new MT2_ServiceApiResponse(true, todo);

        }

        public async Task<MT2_ServiceApiResponse> MT2_UpdateAsync(MT3_MemoDto model)
        {
            try
            {
                var dbToDo = Mapper.Map<MT2_Memo>(model);
                var repository = work.GetRepository<MT2_Memo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbToDo.Id));

                todo.Title= dbToDo.Title;
                todo.Content = dbToDo.Content;
                todo.UpdateDate = DateTime.Now;

                repository.Update(todo);

                if (await work.SaveChangesAsync() > 0)
                {
                    return new MT2_ServiceApiResponse(true, todo);
                }

                return new MT2_ServiceApiResponse(false, "更新数据异常");

            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse(ex.Message);
            }
        }
    }
}
