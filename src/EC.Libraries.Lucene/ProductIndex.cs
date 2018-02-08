using System;

namespace EC.Libraries.Lucene
{
    public class ProductIndex
    {
        /// <summary>
        /// 商品系统编号
        /// </summary>
        public int SysNo { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public string Prices { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
