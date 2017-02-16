using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace webapi.Utility
{
    public class EncryptHelper
    {
        //校验用户名密码（对Session匹配，或数据库数据匹配）
        public static bool ValidateTicket(string encryptToken)
        {
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptToken).UserData;
            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string userName = strTicket.Substring(0, index);
            string password = strTicket.Substring(index + 1);
            //取得session，不通过说明用户退出，或者session已经过期
            var token = HttpContext.Current.Session[userName];
            if (token == null)
            {
                return false;
            }
            //对比session中的令牌
            if (token.ToString() == encryptToken)
            {
                return true;
            }

            return false;

        }

        public static void Encrypt(string userName,string password)
        {
            string UserData = userName + "#" + password;
            //数据放入ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(60), false, UserData);
            //数据加密
            string enyTicket = FormsAuthentication.Encrypt(ticket);

        }
    }
}