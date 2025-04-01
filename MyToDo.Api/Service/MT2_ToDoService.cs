using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyToDo.Shared;
using MyToDo.Shared.Contact;

namespace MyToDo.Api.Service
{
    /// <summary>
    /// 待办事项的实现
    /// </summary>
    public class MT2_ToDoService : MT2_IToDoService
    {
        private readonly UOW_IUnitOfWork work;
        private readonly IMapper Mapper;

        public MT2_ToDoService(UOW_IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            Mapper = mapper;
        }

        public async Task<MT2_ServiceApiResponse> MT2_AddAsync(MT3_ToDoDto model)
        {
            try
            {
                if (model.CreateDate == DateTime.MinValue) { model.CreateDate = DateTime.Now; model.UpdateDate = DateTime.Now; }
                var todo = Mapper.Map<MT2_ToDo>(model);
                await work.GetRepository<MT2_ToDo>().InsertAsync(todo);
                if (await work.SaveChangesAsync() > 0)
                    return new MT2_ServiceApiResponse(true, todo);
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
                var repository = work.GetRepository<MT2_ToDo>();
                var todo = await repository
                    .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(todo);
                if (await work.SaveChangesAsync() > 0)
                    return new MT2_ServiceApiResponse(true, "");
                return new MT2_ServiceApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse(ex.Message);
            }
        }

        public async Task<MT2_ServiceApiResponse> MT2_GetAllAsync(MT3_QueryParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<MT2_ToDo>();
                var todos = await repository.GetPagedListAsync(predicate:
                   x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Title.Contains(parameter.Search),
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize,
                   orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new MT2_ServiceApiResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse(ex.Message);
            }
        }

        public async Task<MT2_ServiceApiResponse> MT2_GetAllAsync(MT3_ToDoParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<MT2_ToDo>();
                var todos = await repository.GetPagedListAsync(predicate:
                   x => (string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Title.Contains(parameter.Search))
                   && (parameter.Status == null ? true : x.Status.Equals(parameter.Status)),
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize,
                   orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new MT2_ServiceApiResponse(true,todos);
            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse(ex.Message);
            }
        }


        public async Task<MT2_ServiceApiResponse> MT2_GetSingleAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<MT2_ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new MT2_ServiceApiResponse(true, todo);
            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse(ex.Message);
            }
        }

        public async Task<MT2_ServiceApiResponse> MT2_Summary()
        {
            try
            {
                //待办事项结果
                var todos = await work.GetRepository<MT2_ToDo>().GetAllAsync(
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));

                //备忘录结果
                var memos = await work.GetRepository<MT2_Memo>().GetAllAsync(
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));

                MT3_SummaryDto summary = new MT3_SummaryDto();
                summary.Sum = todos.Count(); //汇总待办事项数量
                summary.CompletedCount = todos.Where(t => t.Status == 1).Count(); //统计完成数量
                summary.CompletedRatio = (summary.CompletedCount / (double)summary.Sum).ToString("0%"); //统计完成率
                summary.MemoeCount = memos.Count();  //汇总备忘录数量
                summary.ToDoList = new ObservableCollection<MT3_ToDoDto>(Mapper.Map<List<MT3_ToDoDto>>(todos.Where(t => t.Status == 0)));
                summary.MemoList = new ObservableCollection<MT3_MemoDto>(Mapper.Map<List<MT3_MemoDto>>(memos));

                return new MT2_ServiceApiResponse(true, summary);
            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse(false, "");
            }
        }

        public async Task<MT2_ServiceApiResponse> MT2_UpdateAsync(MT3_ToDoDto model)
        {
            try
            {
                var dbToDo = Mapper.Map<MT2_ToDo>(model);
                var repository = work.GetRepository<MT2_ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbToDo.Id));

                todo.Title = dbToDo.Title;
                todo.Content = dbToDo.Content;
                todo.Status = dbToDo.Status;
                todo.UpdateDate = DateTime.Now;

                repository.Update(todo);

                if (await work.SaveChangesAsync() > 0)
                    return new MT2_ServiceApiResponse(true, todo);
                return new MT2_ServiceApiResponse("更新数据异常！");
            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse(ex.Message);
            }
        }

        public async Task<MT2_ServiceApiResponse> Summary()
        {
            try
            {
                //待办事项结果
                var todos = await work.GetRepository<MT2_ToDo>().GetAllAsync(
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));

                //备忘录结果
                var memos = await work.GetRepository<MT2_Memo>().GetAllAsync(
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));

                MT3_SummaryDto summary = new MT3_SummaryDto();
                summary.Sum = todos.Count(); //汇总待办事项数量
                summary.CompletedCount = todos.Where(t => t.Status == 1).Count(); //统计完成数量
                summary.CompletedRatio = (summary.CompletedCount / (double)summary.Sum).ToString("0%"); //统计完成率
                summary.MemoeCount = memos.Count();  //汇总备忘录数量
                summary.ToDoList = new ObservableCollection<MT3_ToDoDto>(Mapper.Map<List<MT3_ToDoDto>>(todos.Where(t => t.Status == 0)));
                summary.MemoList = new ObservableCollection<MT3_MemoDto>(Mapper.Map<List<MT3_MemoDto>>(memos));

                return new MT2_ServiceApiResponse(true, summary);
            }
            catch (Exception ex)
            {
                return new MT2_ServiceApiResponse(false, "");
            }
        }
    }
}