using System;
using System.Web;
using System.Web.Security;
using EC.Libraries.Util;

namespace EC.Libraries.Auth
{
    /// <summary>
    /// 系统用户认证
    /// </summary>
    /// <remarks> 2014-3-21 苟治国 创建 </remarks>
    public class CustomerAuthImpl : ICustomerAuth
    {
        /// <summary>
        /// 写用户认证
        /// </summary>
        /// <param name="customer">用户</param>
        /// <param name="createPersistentCookie">是否创建持久性Cookie</param>
        public void CustomerSignIn(TicketCustomer customer, bool createPersistentCookie)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                customer.SysNo.ToString(),
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                createPersistentCookie,
                JsonUtil.ToJson(customer),
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { HttpOnly = true };
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
        }

        /// <summary>
        /// 获取当前登录用户票据信息
        /// </summary>
        /// <returns></returns>
        public TicketCustomer GetAuthenticatedCustomer()
        {
//#if TEST
//            return new Customer
//            {
//                SysNo =1,
//                Account = "imsuser_test"
//            };
//#endif
            if (HttpContext.Current == null) return null;
            if (!HttpContext.Current.Request.IsAuthenticated || !(HttpContext.Current.User.Identity is FormsIdentity))
            {
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie == null) return null;
                else
                {
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    return JsonUtil.ToObject<TicketCustomer>(ticket.UserData);
                }
            }

            var formsIdentity = (FormsIdentity)HttpContext.Current.User.Identity;

            return JsonUtil.ToObject<TicketCustomer>(formsIdentity.Ticket.UserData);
        }
    }
}
