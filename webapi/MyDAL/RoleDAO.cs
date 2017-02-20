using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using lks.webapi.Extension;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL
{
    public partial class RoleDAO
    {
        public IEnumerable<Role> QueryList2(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total,out string other, bool isDesc = true)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(SqlHelper.GenerateQuerySql("Role", columns, index, size, wheres, orderField, isDesc));
            //string sql = SqlHelper.GenerateQuerySql("Role", columns, index, size, wheres, orderField, isDesc);
            //把所有的路由信息也带出来
            sb.Append("SELECT RouteId, CASE WHEN Op IS NULL then cast(RouteId as nvarchar(50))+'-'+Name  ELSE cast(RouteId as nvarchar(50))+'-'+Name+'-'+Op END as Name FROM dbo.Route;");
            IDataReader reader;
            other = string.Empty;
            var result = SqlHelper.GetList<Role>(sb.ToString(), null, out total, out reader);
            other = reader.ToJson();
            return result;
        }
    }
}