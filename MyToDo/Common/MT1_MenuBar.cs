using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    /// <summary>
    /// 系统导航菜单实体类
    /// </summary>
    public class MT1_MenuBar:BindableBase
    {
        /// <summary>
        /// 菜单图标
        /// </summary>
        private string _icon;
        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// 菜单命名空间
        /// </summary>
        private string _namespace;
        public string Namespace
        {
            get { return _namespace; }
            set { _namespace = value; }
        }


    }
}
