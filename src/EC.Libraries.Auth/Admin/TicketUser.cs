using System.Collections.Generic;
using System.ComponentModel;

namespace EC.Libraries.Auth
{
    /// <summary>
    /// 用户对象
    /// </summary>
    public class TicketUser
    {
        /// <summary>
        /// 系统编号
        /// </summary>
        [Description("系统编号")]
        public int SysNo { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [Description("用户账号")]
        public string Account { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Description("昵称")]
        public string Nickname { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        [Description("真实名称")]
        public string FullName { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        [Description("角色编号")]
        public int RoleSysNo { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        public List<string> PermissionsCode { get; set; }
    }
}
