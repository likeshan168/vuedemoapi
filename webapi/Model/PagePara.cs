using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace lks.webapi.Model
{
    [Serializable]
    public class PagePara
    {
        [DataMember(Name = "index")]
        public int Index { get; set; }
        public int Size { get; set; }
        public string OrderField { get; set; }
        public string WhereStr { get; set; }

        public IEnumerable<string> Columns { get; set; }
    }
}