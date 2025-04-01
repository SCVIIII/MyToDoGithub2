using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyToDo.Api.Service
{
    
    public class MT2_ServiceApiResponse
    {
        public MT2_ServiceApiResponse(string message, bool status = false)
        {
            this.Message = message;
            this.Status = status;
        }

        public MT2_ServiceApiResponse(bool status, object result)
        {
            this.Status = status;
            this.Result = result;
        }

        public string Message { get; set; }

        public bool Status { get; set; }

        public object Result { get; set; }
    }
}
