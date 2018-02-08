using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Libraries.ConsoleApplication
{
    /// <summary>
    /// 中介
    /// </summary>
    public class Intermediary : ILandlady
    {
        private ILandlady _landlady;

        public Intermediary(ILandlady landlady)
        {
            this._landlady = landlady;
        }

        /// <summary>
        /// 中介服务费
        /// </summary>
        public string ServiceCharge()
        {
            return "中介服务费";
        }

        /// <summary>
        /// 房东租金，中介服务费
        /// </summary>
        public string Rent()
        {
            return _landlady.Rent() +","+ ServiceCharge();
        }
    }
}
