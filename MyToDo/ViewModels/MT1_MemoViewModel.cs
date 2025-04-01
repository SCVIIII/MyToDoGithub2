using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class MT1_MemoViewModel: MT1_NavigationViewModel
    {

        private readonly Service.MT1_IMemoService service;
        private readonly MT1_IDialogHostService dialogHost;
        public MT1_MemoViewModel(Service.MT1_MemoService service, IContainerProvider containerProvider) : base(containerProvider)
        {
            MemoDtos = new ObservableCollection<MT3_MemoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            AddCommand = new DelegateCommand(Add);
            SelectCommand = new DelegateCommand<MT3_MemoDto>(Selected);
            DeleteCommand = new DelegateCommand<MT3_MemoDto>(Delete);
            dialogHost = containerProvider.Resolve<MT1_IDialogHostService>();
            this.service = service;
            
        }


        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<MT3_MemoDto> SelectCommand { get; private set; }
        public DelegateCommand<MT3_MemoDto> DeleteCommand { get; private set; }

        

        private async void Delete(MT3_MemoDto dto)
        {

            var dialogResult = await dialogHost.MT1_Question("温馨提示", $"确认删除备忘录:{dto.Title} ?");
            if (dialogResult.Result != ButtonResult.OK) return;
            UpdateLoading(true);

            var deleteResult = await service.MT1_DeleteAsync(dto.Id);
            if (deleteResult.Status)
            {
                var model = MemoDtos.FirstOrDefault(t => t.Id.Equals(dto.Id));
                if (model != null)
                {
                    MemoDtos.Remove(dto);
                }

            }


            UpdateLoading(false);
        }

       

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增": Add(); break;
                case "查询": GetDataAsync(); break;
                case "保存": Save(); break;
            }
        }

        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentDto.Title) ||
                string.IsNullOrWhiteSpace(CurrentDto.Content))
            { return; }

            UpdateLoading(true);

            try
            {
                if (CurrentDto.Id > 0)
                {
                    var updateResult = await service.MT1_UpdateAsync(CurrentDto);
                    if (updateResult.Status)
                    {
                        var todo = _memoDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentDto.Title;
                            todo.Content = CurrentDto.Content;
                        }
                    }
                }
                else
                {
                    var addResult = await service.MT1_AddAsync(CurrentDto);
                    if (addResult.Status)
                    {
                        MemoDtos.Add(addResult.Result);
                        IsRightDrawerOpen = false;
                    }
                }
            } //end of try

            catch (Exception ex) { }

            finally { UpdateLoading(false); }

        }

        /// <summary>
        /// 添加待办
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Add()
        {
            CurrentDto = new MT3_MemoDto();
            IsRightDrawerOpen = true;
        }



        private async void Selected(MT3_MemoDto dto)
        {
            try
            {
                UpdateLoading(true);
                MT3_ApiResponse<MT3_MemoDto> todoResult = await service.MT1_GetFirstOfDefaultAsync(dto.Id);

                if (todoResult.Status)
                {
                    CurrentDto = todoResult.Result;
                    IsRightDrawerOpen = true;
                }

            }
            catch (Exception ex)
            {


            }

            finally
            {
                UpdateLoading(false);
            }


        }

        private bool _isRightDrawerOpen;

        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return _isRightDrawerOpen; }
            set { _isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        private int _selectedIndex;
        /// <summary>
        /// 下拉列表选中状态值
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; RaisePropertyChanged(); }
        }


        private MT3_MemoDto _currentDto;

        /// <summary>
        /// 编辑选中/新增对象
        /// </summary>
        public MT3_MemoDto CurrentDto
        {
            get { return _currentDto; }
            set { _currentDto = value; RaisePropertyChanged(); }
        }
        private string _search;

        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return _search; }
            set { _search = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<MT3_MemoDto> _memoDtos;

        public ObservableCollection<MT3_MemoDto> MemoDtos
        {
            get { return _memoDtos; }
            set { _memoDtos = value; RaisePropertyChanged(); }
        }

        async void GetDataAsync()
        {
            UpdateLoading(true);
            int? Status = SelectedIndex == 0 ? null : SelectedIndex == 2 ? 1 : 0;
            //< Shared.Contact.PagedList < MemoDto >>
            Shared.Contact.MT3_ApiResponse<Shared.Contact.MT3_PagedList<MT3_MemoDto>> todoResult = await service.MT1_GetAllAsync(new MT3_QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search,


            });

            if (todoResult.Status)
            {
                _memoDtos.Clear();
                foreach (var item in todoResult.Result.Items)
                {
                    _memoDtos.Add(item);
                }

            }
            UpdateLoading(false);
        }


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }


    }
}

 
