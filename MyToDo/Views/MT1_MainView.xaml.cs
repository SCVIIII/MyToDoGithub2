using MyToDo.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyToDo.Extensions;
using MyToDo.Common;
using MaterialDesignThemes.Wpf;

namespace MyToDo.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MT1_MainView : Window
    {
        private readonly MT1_IDialogHostService HostService;
        public MT1_MainView(IEventAggregator aggregator,MT1_IDialogHostService hostService)
        {
            InitializeComponent();

            //aggregator.MT1_RegisterMessage(arg =>
            //{
            //    Snackbar.MessageQueue.Enqueue("测试MessageQueue");
            //});

            aggregator.MT1_RegisterMessage(arg =>
            {
                Snackbar.MessageQueue.Enqueue(arg.Message);
            });

            //注册等待信息窗口
            aggregator.MT1_Register(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;

                if(DialogHost.IsOpen)
                {
                    DialogHost.DialogContent = new MT1_ProcessView();

                }

            });
            //IEventAggregator aggregator, Action<MT1_UpdateModel> action
            //aggregator.MT1_Register(action);

            //左侧菜单选中后自动收起
            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };

            //最大最小化
            btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Maximized;
            };
            btnClose.Click += async (s, e) =>
            {
                var dialogResult = await hostService.MT1_Question("温馨提示", "确认退出系统?");
                if (dialogResult.Result != ButtonResult.OK) return;


                this.Close();
            };

            //鼠标拖动与双击
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            ColorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Maximized;
            };
            this.HostService = hostService;
        }

        
    }
}
