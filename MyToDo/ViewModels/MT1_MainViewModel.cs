using MyToDo.Common;
using MyToDo.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class MT1_MainViewModel:BindableBase,IConfigureService
    {
        public MT1_MainViewModel(IContainerProvider containerProvider,IRegionManager regionManager) 
		{ 
            
			MenuBars =new ObservableCollection<MT1_MenuBar>();
            //CreateMenuBar();
            NavigateCommand = new DelegateCommand<MT1_MenuBar>(Navigate);
            this.containerProvider = containerProvider;
            this.regionManager = regionManager;

            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack) { journal.GoBack(); }
            });

            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward) { journal.GoForward(); }
            });

            LoginOutCommand = new DelegateCommand(() =>
            {
                App.LoginOut(containerProvider);
            });


        }

        private void Navigate(MT1_MenuBar obj)
        {
            if(obj == null || string.IsNullOrWhiteSpace(obj.Namespace)) { return; }

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.Namespace,back => 
            {
                journal=back.Context.NavigationService.Journal;
            }
            );
        }
            

        public DelegateCommand<MT1_MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand LoginOutCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }
        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged(); }
        }

        private readonly IContainerProvider containerProvider;
        private readonly IRegionManager regionManager;
        private ObservableCollection<MT1_MenuBar> _menuBars;
        private IRegionNavigationJournal journal;

		public ObservableCollection<MT1_MenuBar> MenuBars
        {
			get { return _menuBars; }
			set { _menuBars = value; RaisePropertyChanged(); }
		}

        void CreateMenuBar()
        {
            MenuBars.Add(new MT1_MenuBar() { Icon = "Home", Title = "首页", Namespace = "MT1_IndexView" });
            MenuBars.Add(new MT1_MenuBar() { Icon = "NotebookOutline", Title = "待办事项", Namespace = "MT1_ToDoView" });
            MenuBars.Add(new MT1_MenuBar() { Icon = "NotebookPlusOutline", Title = "备忘录", Namespace = "MT1_MemoView" });
            MenuBars.Add(new MT1_MenuBar() { Icon = "Cog", Title = "设置", Namespace = "SettingsView" });

        }

        /// <summary>
        /// 配置首页初始化参数
        /// </summary>
        public void Configure()
        {
            UserName = MT1_AppSession.UserName;
            CreateMenuBar();
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("MT1_IndexView");


        }
    }
}
