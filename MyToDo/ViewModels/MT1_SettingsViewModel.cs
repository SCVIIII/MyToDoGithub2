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
    public class MT1_SettingsViewModel:BindableBase
    {
        public MT1_SettingsViewModel(IRegionManager  regionManager) 
        {
            MenuBars = new ObservableCollection<MT1_MenuBar>();
            this.regionManager = regionManager;
            NavigateCommand = new DelegateCommand<MT1_MenuBar>(Navigate);
            CreateMenuBar();
        }

        private void Navigate(MT1_MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.Namespace)) { return; }

            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(obj.Namespace);
    
        }

        public DelegateCommand<MT1_MenuBar> NavigateCommand { get; private set; }
        private readonly IRegionManager regionManager;
        private ObservableCollection<MT1_MenuBar> _menuBars;

        public ObservableCollection<MT1_MenuBar> MenuBars
        {
            get { return _menuBars; }
            set { _menuBars = value; RaisePropertyChanged(); }
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MT1_MenuBar() { Icon = "Palette", Title = "个性化", Namespace = "SkinView" });
            MenuBars.Add(new MT1_MenuBar() { Icon = "Cog", Title = "系统设置", Namespace = "ToDoView" });
            MenuBars.Add(new MT1_MenuBar() { Icon = "Information", Title = "关于更多", Namespace = "AboutView" });

        }
    }
}
