namespace EC.Libraries.Auth
{
    /// <summary>
    /// 用户认证
    /// <para>票据信息</para>
    /// </summary>
    /// <remarks> 2014-3-21 苟治国 创建 </remarks>
    public interface IUserAuth
    {
        /// <summary>
        /// 写用户认证
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="createPersistentCookie">是否创建持久性Cookie</param>
        void SignIn(TicketUser user, bool createPersistentCookie);

        /// <summary>
        /// 登出认证
        /// </summary>
        void SignOut();

        /// <summary>
        /// 获取用户认证
        /// </summary>
        /// <returns></returns>
        TicketUser GetAuthenticatedUser();
    }
}
