using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace lks.webapi.Utility
{
    public class Authentication
    {
        /// <summary>
        /// 设置用户登陆成功凭据（Cookie存储）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="rights">权限</param>
        public static void SetCookie(string userName, string password, string rights)
        {
            string userData = string.Empty;
            if (string.IsNullOrWhiteSpace(rights))
            {
                userData = userName + "#" + password;
            }
            else
            {
                userData = userName + "#" + password + "#" + rights;
            }
            //数据放入ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(60), false, userData);
            //数据加密
            string enyTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, enyTicket);
            //防止浏览器攻击窃取、伪造Cookie(这个只能在服务端进行访问)
            cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(cookie);

        }

        public static string GetEncryptStr(string userName, string password, string rights)
        {
            string userData = string.Empty;
            if (string.IsNullOrWhiteSpace(rights))
            {
                userData = userName + "#" + password;
            }
            else
            {
                userData = userName + "#" + password + "#" + rights;
            }
            //数据放入ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(60), false, userData);
            //数据加密
            string enyTicket = FormsAuthentication.Encrypt(ticket);
            return enyTicket;

        }
        /// <summary>
        /// 判断用户是否登陆
        /// </summary>
        /// <returns>True,Fales</returns>
        public static bool isLogin()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        /// <summary>
        /// 注销登陆
        /// </summary>
        public static void logOut()
        {
            FormsAuthentication.SignOut();
        }
        /// <summary>
        /// 获取凭据中的用户名
        /// </summary>
        /// <returns>用户名</returns>
        public static string GetUserName()
        {
            if (isLogin())
            {
                string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
                string[] UserData = strUserData.Split('#');
                if (UserData.Length != 0)
                {
                    return UserData[0].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取凭据中的密码
        /// </summary>
        /// <returns>密码</returns>
        public static string GetPassWord()
        {
            if (isLogin())
            {
                string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
                string[] UserData = strUserData.Split('#');
                if (UserData.Length != 0)
                {
                    return UserData[1].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取凭据中的用户权限
        /// </summary>
        /// <returns>用户权限</returns>
        public static string GetRights()
        {
            if (isLogin())
            {
                string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
                string[] UserData = strUserData.Split('#');
                if (UserData.Length != 0)
                {
                    return UserData[2].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}