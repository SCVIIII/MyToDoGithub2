using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels.Dialog
{
    public class MT1_AddToDoViewModel :BindableBase, MT1_IDialogHostAware
    {
        public MT1_AddToDoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No)); //取消返回NO告诉操作结束
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Model.Title) ||
                string.IsNullOrWhiteSpace(model.Content)) return;

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                //确定时,把编辑的实体返回并且返回OK
                DialogParameters param = new DialogParameters();
                param.Add("Value", Model);
                var dialogResult = new DialogResult()
                {
                    Result = ButtonResult.OK,
                    Parameters = param
                };
                DialogHost.Close(DialogHostName, dialogResult);
            }
        }

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        private MT3_ToDoDto model;
        /// <summary>
        /// 新增或编辑的实体
        /// </summary>
        public MT3_ToDoDto Model
        {
            get { return model; }
            set { model = value;  RaisePropertyChanged(); }
        }



        public void OnDialogOpen(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                Model = parameters.GetValue<MT3_ToDoDto>("Value");
            }
            else
                Model = new MT3_ToDoDto();
        }
    }
}
