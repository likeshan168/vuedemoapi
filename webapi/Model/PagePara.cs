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
        /// <summary>
        /// 需要查询的列，没有提供的话，默认是所有的列
        /// </summary>
        public IEnumerable<string> Columns { get; set; }
        /// <summary>
        /// 是否还需获取其他表的信息
        /// </summary>
        public bool Other { get; set; }
    }
}