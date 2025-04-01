using MyToDo.Api.Service;
using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using Prism.Dialogs;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace MyToDo.ViewModels
{
    public class MT1_IndexViewModel : MT1_NavigationViewModel
    {
        private readonly MT1_IToDoService toDoService;
        private readonly MT1_IMemoService meMoService;
        private readonly MT1_IDialogHostService dialog;
        private readonly IRegionManager regionManager;

        public DelegateCommand<string> ExecuteCommand { get; set; }
        public DelegateCommand<MT3_ToDoDto> ToDoCompletedCommand { get; private set; }
        public DelegateCommand<MT3_ToDoDto> EditToDoCommand { get;private set; }
        public DelegateCommand<MT3_MemoDto> EditMemoCommand { get; private set; }
        public DelegateCommand<MT1_TaskBar> NavigateCommand { get; private set; }


        public MT1_IndexViewModel(IContainerProvider provider, MT1_IDialogHostService dialog) : base(provider)
        {
            Title = $"你好,{MT1_AppSession.UserName}{DateTime.Now.GetDateTimeFormats('D')[1].ToString()}";
            CreaterTaskBars();
            ExecuteCommand = new DelegateCommand<string>(Excute);
            EditMemoCommand = new DelegateCommand<MT3_MemoDto>(AddMemo);
            EditToDoCommand = new DelegateCommand<MT3_ToDoDto>(AddToDo);
            ToDoCompletedCommand = new DelegateCommand<MT3_ToDoDto>(Completed);
            NavigateCommand = new DelegateCommand<MT1_TaskBar>(Navigate);
            this.toDoService = provider.Resolve<MT1_IToDoService>();
            this.meMoService = provider.Resolve<MT1_IMemoService>();
            this.dialog = dialog;
            this.regionManager = provider.Resolve<IRegionManager>();
        }

        private void Navigate(MT1_TaskBar obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Target)) return;

            NavigationParameters param = new NavigationParameters();

            if (obj.Title == "已完成")
            {
                param.Add("Value", 2);
            }
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.Target, param);
        }

        private async void Completed(MT3_ToDoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var updateResult = await toDoService.MT1_UpdateAsync(obj);
                if (updateResult.Status)
                {
                    var todo = summary.ToDoList.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (obj != null)
                    {
                        summary.ToDoList.Remove(obj);
                        summary.ToDoList.Remove(todo);
                        summary.CompletedCount += 1;
                        summary.CompletedRatio = (summary.CompletedCount / (double)summary.Sum).ToString("0%");
                        this.Refresh();
                    }
                    aggregator.MT1_SendMessage("已完成!");
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private void Excute(string obj)
        {
            switch (obj)
            {
                case "新增待办": AddToDo(null); break;
                case "新增备忘录": AddMemo(null); break;
            }
        }

        /// <summary>
        /// 添加待办事项
        /// </summary>
        async void AddToDo(MT3_ToDoDto model)
        {

            DialogParameters param = new DialogParameters();
            if (model != null)
                //model.CreateDate = DateTime.Now;
                param.Add("Value", model);
            var dialogResult = await dialog.MT1_ShowDialog("MT1_AddToDoView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                try
                {
                    UpdateLoading(true);
                    var todo = dialogResult.Parameters.GetValue<MT3_ToDoDto>("Value");
                    if (todo.Id > 0)
                    {
                        var updateResult = await toDoService.MT1_UpdateAsync(todo);
                        if (updateResult.Status)
                        {
                            var todoModel = Summary.ToDoList.FirstOrDefault(t => t.Id.Equals(todo.Id));
                            //var todoModel = summary.ToDoList.FirstOrDefault(t => t.Id.Equals(todo.Id));
                            if (todoModel != null)
                            {
                                todoModel.Title = todo.Title;
                                todoModel.Content = todo.Content;
                            }
                        }
                    }
                    else
                    {
                        var addResult = await toDoService.MT1_AddAsync(todo);
                        if (addResult.Status)
                        {
                            summary.Sum += 1;
                            summary.ToDoList.Add(addResult.Result);
                            summary.CompletedRatio = (summary.CompletedCount / (double)summary.Sum).ToString("0%");
                            this.Refresh();
                        }
                    }
                }
                finally
                {
                    UpdateLoading(false);
                }
            }
        }

        async void AddMemo(MT3_MemoDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            var dialogResult = await dialog.MT1_ShowDialog("MT1_AddMemoView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                try
                {
                    UpdateLoading(true);
                    var memo = dialogResult.Parameters.GetValue<MT3_MemoDto>("Value");
                    if (memo.Id > 0)
                    {
                        var updateResult = await meMoService.MT1_UpdateAsync(memo);
                        if (updateResult.Status)
                        {
                            var memoModel = Summary.MemoList.FirstOrDefault(t => t.Id.Equals(memo.Id));
                            //var memoModel = summary.ToDoList.FirstOrDefault(t => t.Id.Equals(memo.Id));
                            if (memoModel != null)
                            {
                                memoModel.Title = memo.Title;
                                memoModel.Content = memo.Content;
                            }
                        }

                    }
                    else
                    {
                        var addResult = await meMoService.MT1_AddAsync(memo);
                        if (addResult.Status)
                        {
                            Summary.MemoList.Add(addResult.Result);
                            
                        }
                    }
                }
                finally
                {
                    UpdateLoading(false);
                }
            }
        }


        

        #region 属性
        private ObservableCollection<MT1_TaskBar> _taskBars;
        public ObservableCollection<MT1_TaskBar> TaskBars
        {
            get { return _taskBars; }
            set { _taskBars = value; RaisePropertyChanged(); }
        }

        private MT3_SummaryDto summary;
        /// <summary>
        /// 首页统计
        /// </summary>
        public MT3_SummaryDto Summary
        {
            get { return summary; }
            set { summary = value; RaisePropertyChanged(); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value;  RaisePropertyChanged(); }
        }


        #endregion


        void CreaterTaskBars()
        {
            TaskBars = new ObservableCollection<MT1_TaskBar>();
            TaskBars.Add(new MT1_TaskBar() { Icon = "ClockFast", Title = "汇总", Color = "#FF0CA0FF", Target = "MT1_ToDoView" });
            TaskBars.Add(new MT1_TaskBar() { Icon = "ClockCheckOutline", Title = "已完成",  Color = "#FF1ECA3A", Target = "MT1_ToDoView" });
            TaskBars.Add(new MT1_TaskBar() { Icon = "ChartLineVariant", Title = "完成比例",  Color = "#FF02C6DC", Target = "" });
            TaskBars.Add(new MT1_TaskBar() { Icon = "PlaylistStar", Title = "备忘录",  Color = "#FFFFA000", Target = "MT1_MemoView" });
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var summaryResult = await toDoService.SummaryAsync();
            if (summaryResult.Status)
            {
                Summary = summaryResult.Result;
                Refresh();
            }
            base.OnNavigatedTo(navigationContext);
        }

        void Refresh()
        {
            TaskBars[0].Content = summary.Sum.ToString();
            TaskBars[1].Content = summary.CompletedCount.ToString();
            TaskBars[2].Content = summary.CompletedRatio;
            TaskBars[3].Content = summary.MemoeCount.ToString();
        }
    }
}

