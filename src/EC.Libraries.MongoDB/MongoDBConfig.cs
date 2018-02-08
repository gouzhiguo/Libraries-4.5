using EC.Libraries.Framework;

namespace EC.Libraries.MongoDB
{
    /// <summary>
    /// 存放NoSqlDB的连接配置信息
    /// </summary>
    public class MongoDBConfig : BaseConfig
    {
        /// <summary>
        /// NoSqlDB服务器的连接参数
        /// 格式为：mongodb://[user:password@]<host>:<port>
        /// </summary>
        public string Host{ set; get; }

        /// <summary>
        /// 使用的数据库名
        /// </summary>
        public string DBName{ set; get; }

        /// <summary>
        /// 超时
        /// </summary>
        public int TimeOut { set; get; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { set; get; }
    }
}
