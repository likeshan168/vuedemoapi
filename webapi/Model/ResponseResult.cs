using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lks.webapi.Model;

namespace lks.webapi.Model
{
    public class ResponseResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }

        public UserInfo User { get; set; }
    }
}