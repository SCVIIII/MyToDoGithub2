using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class MT1_LoginViewModel : BindableBase, IDialogAware
    {
        private readonly IEventAggregator _aggregator;
        public MT1_LoginViewModel(MT1_LoginService loginService, IEventAggregator aggregator)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this._loginService = loginService;
            UserDto = new MT3_RegisterUserDto();
            this._aggregator = aggregator;
        }
        public string Title { get; set; } = "ToDo";

        public DialogCloseListener RequestClose { get; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            //引入条件检查,登录成功时，返回OK
            if(!_isLoginSuccessful)
            {
                _isLoginSuccessful = false;
                return;
            }
            else
            {
                LoginOut();
            }
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        private bool _isLoginSuccessful = false;
        private string _account;

        public string Account
        {
            get { return _account; }
            set { _account = value; RaisePropertyChanged(); }
        }

        private int _selectIndex;

        public int SelectIndex
        {
            get { return _selectIndex; }
            set { _selectIndex = value; RaisePropertyChanged(); }
        }

        private readonly MT1_LoginService _loginService;
        private string _passWord;
        public string Password
        {
            get { return _passWord; }
            set { _passWord = value; RaisePropertyChanged(); }
        }

        private MT3_RegisterUserDto _userDto;

        public MT3_RegisterUserDto UserDto
        {
            get { return _userDto; }
            set { _userDto = value; RaisePropertyChanged(); }
        }


        void Execute(string arg)
        {

            switch (arg)
            {
                case "Login"    :   Login();            break;
                case "LoginOut" :   LoginOut();         break;
                    //跳转至注册页面
                case "GO"       :   SelectIndex = 1;    break;
                    //返回登录页面
                case "Return"   :   SelectIndex = 0;    break;
                    //注册账号
                case "Register" :   Register();         break;
            }
        }

        private async void Register()
        {
            if (string.IsNullOrWhiteSpace(UserDto.Account) ||
                string.IsNullOrWhiteSpace(UserDto.UserName) ||
                string.IsNullOrWhiteSpace(UserDto.Password) ||
                string.IsNullOrWhiteSpace(UserDto.NewPassword))
            {
                _aggregator.MT1_SendMessage("请输入完整的注册信息！", "Login");
                return;
            }

            if (UserDto.Password != UserDto.NewPassword)
            {
                _aggregator.MT1_SendMessage("密码不一致,请重新输入！", "Login");
                return;
            }

            var registerResult = await _loginService.MT1_RegisterAsync(new Shared.Dtos.MT3_UserDto()
            {
                Account = UserDto.Account,
                UserName = UserDto.UserName,
                Password = UserDto.Password
            });

            if (registerResult != null && registerResult.Status)
            {
                _aggregator.MT1_SendMessage("注册成功", "Login");
                //注册成功,返回登录页页面
                SelectIndex = 0;
            }
            else
            {
                //注册失败提示
                _aggregator.MT1_SendMessage(registerResult.Message);

            }
        }

        async void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) ||
                string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            var loginResult = await _loginService.MT1_LoginAsync(Account,Password);
            //240911
            //var loginResult = await _loginService.MT1_LoginAsync(new Shared.Dtos.MT3_UserDto()
            //{
            //    Account = Account,
            //    Password = Password,
            //    //UserName ="1231231"
            //});

            if (loginResult != null && loginResult.Status)
            {
                MT1_AppSession.UserName= loginResult.Result.UserName;
                RequestClose.Invoke(new DialogResult(ButtonResult.OK));
                //引入条件检查
                _isLoginSuccessful = true;
            }

            
            else
            {
                //登录失败提示...
                _aggregator.MT1_SendMessage(loginResult.Message, "Login");
            }

        }

        private void LoginOut()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.No));
        }

    }
}
