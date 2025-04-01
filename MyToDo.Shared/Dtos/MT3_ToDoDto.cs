using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    /// <summary>
    /// 待办事项数据实体
    /// </summary>
    public  class MT3_ToDoDto:MT3_BaseDto
    {
        private string _title;
        private string _content;
        private int _status;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        public string Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged(); }
        }
        public int Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }

    }
}
