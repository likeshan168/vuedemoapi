using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
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
    }
}