using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lks.webapi.Model
{
    public class PagePara
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public string OrderField { get; set; }
        public object WhereStr { get; set; }
    }
}