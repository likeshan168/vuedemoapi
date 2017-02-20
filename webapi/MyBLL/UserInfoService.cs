using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using lks.webapi.DAL;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.BLL
{
    public partial class UserInfoService
    {

        public dynamic Exists(string userName, string password)
        {
            try
            {
                var routes = dal.Check(userName, password);
                if (routes.Count() != 0)
                {

                    //跨域不能用cookie的方式
                    //Authentication.SetCookie(userName, password, null);
                    string str = Authentication.GetEncryptStr(userName, password, null);
                    MemoryCacher.Add(userName, str, DateTimeOffset.UtcNow.AddHours(1));
                    MemoryCacher.Add(userName + "_" + password, routes, DateTimeOffset.UtcNow.AddHours(1));
                    //session的原理就是利用cookie的
                    //HttpContext.Current.Session["CurrentUser"] = str;
                    return new ResponseResult()
                    {
                        Code = 200,
                        Msg = "请求成功",
                        User = new UserInfo() { Name = userName, Password = password },
                        AccessToken = str,
                        Routes = routes
                    };
                }
                else
                {
                    return new ResponseResult()
                    {
                        Code = 401,
                        Msg = "不存在",
                        User = new UserInfo() { Name = userName, Password = password }
                    };
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["CurrentUser"] = null;
                return new ResponseResult()
                {
                    Code = 500,
                    Msg = ex.Message,
                    User = new UserInfo() { Name = userName, Password = password }
                };
            }
        }
        public dynamic GetList(string queryStr, IEnumerable<string> columns, int index, int size, string whereStr, string orderField, out int total, bool isDesc = true)
        {
            try
            {
                var queryList = QuerySringHelper.ResolveQuery(queryStr);
                string ticket = queryList["a"];
                string name = GetKey(ticket);

                //从缓存中取出token
                var routes = (IEnumerable<Route>)MemoryCacher.GetValue(name);
                var route = routes.FirstOrDefault(r => r.Name == "用户列表" && r.Op == "c");
                IEnumerable<UserInfo> users;
                if (route == null)
                {
                    users = dal.QueryList2(columns, index, 1, whereStr, orderField, out total);
                }
                else
                {
                    users = dal.QueryList2(columns, index, size, whereStr, orderField, out total);
                }
                return new
                {
                    Users = users,
                    Total = total,
                    Code = 200,
                    Msg = "请求成功"
                };
            }
            catch (Exception ex)
            {
                total = 0;
                return new
                {
                    Users = new List<UserInfo>(),
                    Total = total,
                    Code = 500,
                    Msg = ex.Message
                };
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public dynamic AddModel(UserInfo model)
        {
            try
            {
                dal.Add(model);
                return new ResponseResult()
                {
                    Code = 200,
                    Msg = "请求成功",
                    User = model
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult()
                {
                    Code = 500,
                    Msg = ex.Message,
                    User = model
                };
            }
        }

        private static string GetKey(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
                return string.Empty;

            string userData = FormsAuthentication.Decrypt(ticket).UserData;
            string[] users = userData.Split('#');
            if (users.Length != 0)
            {
                return users[0] + "_" + users[1];
            }
            else
            {
                return string.Empty;
            }
        }
    }
}