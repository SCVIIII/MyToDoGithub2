
using MaterialDesignThemes.Wpf;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json.Linq;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyToDo.Common
{
    /// <summary>
    /// 自定义对话主机服务
    /// </summary>
    public class MT1_DialogHostService : DialogService,MT1_IDialogHostService
    {
        private readonly IContainerExtension containerExtension;

        public MT1_DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }


        public async Task<IDialogResult> MT1_ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root")

        {
            if (parameters == null)
                parameters = new DialogParameters();


            //从容器当中取出弹出窗口的实例
            var content = containerExtension.Resolve<object>(name);

            //验证实例的有效性
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
            {
                ViewModelLocator.SetAutoWireViewModel(view, true);

            }

            if (!(dialogContent.DataContext is MT1_IDialogHostAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            viewModel.DialogHostName = dialogHostName;

            DialogOpenedEventHandler handler = (sender, eventArgs) =>
            {
                if (viewModel is MT1_IDialogHostAware dialogHostAware)
                {
                    dialogHostAware.OnDialogOpen(parameters);
                }
                eventArgs.Session.UpdateContent(content);
            };

            var returnresult = (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, handler);
            return returnresult;
            //return (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, handler);

        }
    }
}
