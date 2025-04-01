using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class MT3_UserDto:MT3_BaseDto
    {

		private string _username;

		public string UserName
		{
			get { return _username; }
			set { _username = value;
                OnPropertyChanged();
            }
		}

        private string _account;

        public string Account
        {
            get { return _account; }
            set { _account = value; 
                OnPropertyChanged(); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value;
                OnPropertyChanged();
            }
        }

    }
}
