using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.DAL
{
    public partial class MonthConfigDAO
    {
        public void AddAndUpdate(IEnumerable<MonthConfig> models)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (MonthConfig item in models)
            {
               
                if (!string.IsNullOrWhiteSpace(item.委托人简称))
                    strSql.Append($"UPDATE MonthConfig SET 月数={item.月数} WHERE 委托人简称='{item.委托人简称}';");
            }

            SqlHelper.ExecuteSql(strSql.ToString());
        }
    }
}