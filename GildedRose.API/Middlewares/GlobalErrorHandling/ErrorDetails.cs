using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GildedRose.API.Middlewares.GlobalErrorHandling
{
    public class ErrorDetails
    {
        public string ErrorCode { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
