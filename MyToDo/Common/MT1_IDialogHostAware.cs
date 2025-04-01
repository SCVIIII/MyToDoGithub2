using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    public interface MT1_IDialogHostAware
    {
        /// <summary>
        /// Dialoghost名称
        /// </summary>
        string DialogHostName {  get; set; }
        /// <summary>
        /// 打开过程中执行
        /// </summary>
        /// <param name="parameters"></param>
        void OnDialogOpen(IDialogParameters parameters);
        /// <summary>
        /// 确定命令
        /// </summary>
        DelegateCommand SaveCommand { get; set; }
        /// <summary>
        /// 取消命令
        /// </summary>
        DelegateCommand CancelCommand { get; set; }

        
    }
}
