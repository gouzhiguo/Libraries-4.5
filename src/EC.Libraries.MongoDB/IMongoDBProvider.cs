using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace EC.Libraries.MongoDB
{
    using EC.Libraries.Framework;

    /// <summary>
    /// 对外公开MongoDB查询接口
    /// </summary>
    public interface IMongoDBProvider : IProxyBaseObject<IMongoDBProvider>
    {
        /// <summary>  
        /// 将数据插入进数据库  
        /// </summary>  
        /// <typeparam name="T">需要插入数据库的实体类型</typeparam>  
        /// <param name="t">需要插入数据库的具体实体</param>  
        /// <param name="collectionName">指定插入的集合</param> 
        bool Insert<T>(T t, string collectionName);

        /// <summary>  
        /// 批量插入数据  
        /// </summary>  
        /// <typeparam name="T">需要插入数据库的实体类型</typeparam>  
        /// <param name="list">需要插入数据的列表</param>  
        /// <param name="collectionName">指定要插入的集合</param>  
        bool Insert<T>(List<T> list, string collectionName);

        /// <summary>  
        /// 查询一条记录(http://blog.csdn.net/heyangyi_19940703/article/details/51192854)
        /// </summary>  
        /// <typeparam name="T">该数据所属的类型</typeparam>  
        /// <param name="query">查询的条件 可以为空</param>  
        /// <param name="collectionName">去指定查询的集合</param>  
        /// <returns>返回一个实体类型</returns>  
        T FindOne<T>(IMongoQuery query, string collectionName);
    }
}
