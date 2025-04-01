using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    public class MT1_MemoDto:MT1_BaseDto
    {
        /// <summary>
        /// 备忘录
        /// </summary>
        private int _status;
        private string _content;
        private string _title;


        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }


        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }


        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
