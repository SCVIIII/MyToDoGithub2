using MyToDo.Common;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    //internal class MT1_MemoService
    //{
    //}
    public class MT1_MemoService : MT1_BaseService<MT3_MemoDto>, MT1_IMemoService
    {
        public MT1_MemoService(MT1_HttpRestClient client) : base(client, "MT2_Memo")
        {
        }

        
    }
}
