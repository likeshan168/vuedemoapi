using System.Collections.Generic;
using System.Data;
using System.Text;
using lks.webapi.Extension;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL
{
    public partial class CommissionDAO
    {
        public IEnumerable<Commission> QueryList2(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, out string other, bool isDesc = true)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(SqlHelper.GenerateQuerySql("Commission", columns, index, size, wheres, orderField, isDesc));
            //把所有的路由信息也带出来
            sb.Append("SELECT * FROM [dbo].[MonthConfig];");
            IDataReader reader;
            other = string.Empty;
            var result = SqlHelper.GetList<Commission>(sb.ToString(), null, out total, out reader);
            other = reader.ToJson();
            return result;
        }
    }
}