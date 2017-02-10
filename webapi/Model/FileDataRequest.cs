using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lks.webapi.Model
{
    public class FileDataRequest
    {
        /// <summary>
        /// 列数，用这个来区分不同的excel文件
        /// </summary>
        public int ColumnCount { get; set; }
        /// <summary>
        /// excel中的数据
        /// </summary>
        public IList<Commission> Commissions { get; set; }
    }
}