using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Libraries.ConsoleApplication
{
    /// <summary>
    /// 房东接口实现
    /// </summary>
    public class Landlady : ILandlady
    {
        /// <summary>
        /// 租金
        /// </summary>
        public string Rent()
        {
            return "房东收取租金";
        }
    }
}
