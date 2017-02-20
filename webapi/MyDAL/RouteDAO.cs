using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL
{
    public partial class RouteDAO
    {
        public dynamic GetRouteByRoleId(int roleId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DECLARE @RouteId NVARCHAR(400),@sql varchar(1000);");
            sb.Append($"SELECT @RouteId = RouteId FROM dbo.role WHERE RoleId = {roleId};");
            sb.Append("SET @sql = 'SELECT RouteId, CASE WHEN Op IS NULL then cast(RouteId as nvarchar(50))+''-''+Name  ELSE cast(RouteId as nvarchar(50))+''-''+Name+''-''+Op END as Name FROM dbo.Route  WHERE RouteId IN(' + @routeId +')';");
            sb.Append("EXEC (@sql)");
            return SqlHelper.GetList<Route>(sb.ToString(), null);
        }
    }
}