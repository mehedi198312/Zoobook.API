using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoobook.Model
{
    public class BaseResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<object> DataList { get; set; }
    }
}