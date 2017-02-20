using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lks.webapi.Model;

namespace lks.webapi.BLL
{
    public partial class RouteService
    {
        public dynamic QueryList2(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            total = 0;
            try
            {
                var roles = dal.QueryList(columns, index, size, wheres, orderField, out total, isDesc);
                return new
                {
                    Routes = roles,
                    Total = total,
                    Code = 200,
                    Msg = "请求成功"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Routes = new List<Route>(),
                    Total = total,
                    Code = 500,
                    Msg = ex.Message
                };
            }
        }

        public dynamic GetRouteByRoleId(int roleId)
        {
            try
            {
                var routes = dal.GetRouteByRoleId(roleId);
                return new
                {
                    Routes = routes,
                    Code = 200,
                    Msg = "请求成功"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Routes = new List<Route>(),
                    Code = 500,
                    Msg = ex.Message
                };
            }
        }
        public dynamic AddModel(Route model)
        {
            try
            {
                dal.Add(model);
                return new 
                {
                    Code = 200,
                    Msg = "请求成功",
                    Route = model
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Code = 500,
                    Msg = ex.Message,
                    Route = model
                };
            }
        }
    }
}