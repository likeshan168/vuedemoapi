using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lks.webapi.Utility
{
    public class QuerySringHelper
    {
        public static Dictionary<string, string> ResolveQuery(string query)
        {
            string q = query.TrimStart('?');
            string[] qarr = q.Split('&');
            Dictionary<string, string> queryList = new Dictionary<string, string>();
            string[] queryItem;
            foreach (string item in qarr)
            {
                queryItem = item.Split('=');
                if (queryItem.Length != 0 && !string.IsNullOrWhiteSpace(queryItem[0]))
                {
                    queryList.Add(queryItem[0], queryItem[1]);
                }
            }

            return queryList;
        }
    }
}