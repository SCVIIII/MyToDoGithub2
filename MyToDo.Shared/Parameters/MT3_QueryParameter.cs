﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Parameters
{
    public class MT3_QueryParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; } = string.Empty;
    }
}
