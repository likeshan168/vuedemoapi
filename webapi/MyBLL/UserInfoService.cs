using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lks.webapi.DAL;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace lks.webapi.BLL
{
    public partial class UserInfoService
    {
        private readonly UserInfoDAO _dal = new UserInfoDAO();

        public dynamic Exists(string userName, string password)
        {
            try
            {
                var routes = _dal.Check(userName, password);
                if (routes.Count() != 0)
                {
                    
                    //跨域不能用cookie的方式
                    //Authentication.SetCookie(userName, password, null);
                    string str = Authentication.GetEncryptStr(userName, password, null);
                    MemoryCacher.Add(userName, str, DateTimeOffset.UtcNow.AddHours(1));
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
                    HttpContext.Current.Session["CurrentUser"] = null;
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
    }
}