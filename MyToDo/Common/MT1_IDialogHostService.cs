using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    public interface  MT1_IDialogHostService:IDialogService
    {

        Task<IDialogResult> MT1_ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root");


    }
}
