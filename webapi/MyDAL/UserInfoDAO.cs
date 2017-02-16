using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL
{
    public partial class UserInfoDAO
    {
        public bool Exists(string name, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where ");
            strSql.Append(" name = @Name or email=@name or phoneNumber=@name and password=@Password  ");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Name", SqlDbType.NVarChar,20),
                    new SqlParameter("@Password", SqlDbType.NVarChar,20),

                        };
            parameters[0].Value = name;
            parameters[1].Value = password;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }
        public IEnumerable<Route> Check(string name, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @routeId NVARCHAR(100),@sql varchar(1000);");
            strSql.Append($"SELECT @routeId= r.RouteId FROM dbo.UserInfo u LEFT JOIN dbo.Role r ON r.RoleId = u.RoleId WHERE u.Name='{name}' AND u.Password='{password}';");
            strSql.Append("SET @sql = 'SELECT * FROM dbo.Route WHERE RouteId IN(' + @routeId +')';");
            strSql.Append("EXEC (@sql)");
            return SqlHelper.GetList<Route>(strSql.ToString(), null);
        }
    }
}