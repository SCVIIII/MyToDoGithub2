using System.Configuration;
using System.Data;
using System.Windows;
using MyToDo.Common;
using MyToDo.Service;
using MyToDo.ViewModels;
using MyToDo.ViewModels.Dialog;
using MyToDo.Views;
using MyToDo.Views.Dialog;
using Prism.Dialogs;
using Prism.DryIoc;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MT1_MainView>();
        }

        public static void LoginOut(IContainerProvider containerProvider)
        {
            Current.MainWindow.Hide();

            var dialog = containerProvider.Resolve<IDialogService>();
            dialog.ShowDialog("MT1_LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    //Environment.Exit(0);
                    Application.Current.Shutdown();
                    return;
                }

                Current.MainWindow.Show();

            });
            
        }

        protected override void OnInitialized()
        {
            //var service = App.Current.MainWindow.DataContext as IConfigureService;
            //if (service != null)
            //{
            //    service.Configure();
            //}
            //base.OnInitialized();

            //240910
            var dialog = Container.Resolve<IDialogService>();
            dialog.ShowDialog("MT1_LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    //Environment.Exit(0);
                    Application.Current.Shutdown();
                    return;
                }

                
            });
            var service = App.Current.MainWindow.DataContext as IConfigureService;
            if (service != null)
            {
                service.Configure();
            }
            base.OnInitialized();

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().Register<MT1_HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:35275/", serviceKey: "webUrl");
            containerRegistry.Register<MT1_ILoginService, MT1_LoginService>();
            containerRegistry.Register<MT1_IToDoService, MT1_ToDoService>();
            containerRegistry.Register<MT1_IMemoService, MT1_MemoService>();
            containerRegistry.Register<MT1_IDialogHostService, MT1_DialogHostService>();

            containerRegistry.RegisterForNavigation<MT1_AddToDoView, MT1_AddToDoViewModel>();
            containerRegistry.RegisterForNavigation<MT1_AddMemoView, MT1_AddMemoViewModel>();
            containerRegistry.RegisterForNavigation<MT1_MsgView, MT1_MsgViewModel>();
            containerRegistry.RegisterDialog<MT1_LoginView, MT1_LoginViewModel>();


            containerRegistry.RegisterForNavigation<SkinView, MT1_SkinViewModel>();

            containerRegistry.RegisterForNavigation<MT1_IndexView,MT1_IndexViewModel>();
            containerRegistry.RegisterForNavigation<MT1_MemoView, MT1_MemoViewModel>();
            containerRegistry.RegisterForNavigation<MT1_ToDoView, MT1_ToDoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, MT1_SettingsViewModel>();
            containerRegistry.RegisterForNavigation<AboutView>();

        
        }
    }
}