using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Events
{

    public class MT1_UpdateModel
    { public bool IsOpen { get; set; } }
    public class MT1_UpdateLoadingEvent:PubSubEvent<MT1_UpdateModel>

    {


    }
}
