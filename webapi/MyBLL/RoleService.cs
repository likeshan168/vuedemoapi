using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lks.webapi.DAL;
using lks.webapi.Model;

namespace lks.webapi.BLL
{
    public partial class RoleService
    {
        public dynamic QueryList2(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            total = 0;
            try
            {
                var roles = dal.QueryList(columns, index, size, wheres, orderField, out total, isDesc);
                return new
                {
                    Roles = roles,
                    Total = total,
                    Code = 200,
                    Msg = "请求成功"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Roles = new List<Role>(),
                    Total = total,
                    Code = 500,
                    Msg = ex.Message
                };
            }

        }

        public dynamic QueryList3(IEnumerable<string> columns, int index, int size, string wheres, string orderField, out int total, bool isDesc = true)
        {
            total = 0;
            string other = string.Empty;
            try
            {
                var roles = dal.QueryList2(columns, index, size, wheres, orderField, out total, out other, isDesc);
                return new
                {
                    Roles = roles,
                    Total = total,
                    Code = 200,
                    Msg = "请求成功",
                    Other = other
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Roles = new List<Role>(),
                    Total = total,
                    Code = 500,
                    Msg = ex.Message,
                    Other = other
                };
            }

        }

        public dynamic AddModel(Role model)
        {
            try
            {
                dal.Add(model);
                return new
                {
                    Code = 200,
                    Msg = "请求成功",
                    Role = model
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Code = 500,
                    Msg = ex.Message,
                    Role = model
                };
            }
        }
    }
}