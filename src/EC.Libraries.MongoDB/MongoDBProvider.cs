using System;
using System.Collections.Generic;
using EC.Libraries.Framework;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;


namespace EC.Libraries.MongoDB
{
    /// <summary>
    /// MongoDB查询提供类（http://blog.csdn.net/heyangyi_19940703/article/details/51192854）
    /// </summary>
    internal class MongoDBProvider:IMongoDBProvider
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private static object _lock = new object();
        
        /// <summary>
        /// Redis配置
        /// </summary>
        private static MongoDBConfig _mongoDBConfig = null;

        /// <summary>
        /// Database
        /// </summary>
        private MongoDatabase _db = null;

        /// <summary>  
        /// ObjectId的键  
        /// </summary>  
        private readonly string OBJECTID_KEY = "_id";

        /// <summary>
        /// 获取所需的基础调用实体
        /// </summary>
        public IMongoDBProvider Instance
        {
            get { return this; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        public void Initialize(BaseConfig config = null)
        {
            lock (_lock)
            {
                if (config != null) _mongoDBConfig = config as MongoDBConfig;

                if (_mongoDBConfig == null)
                {
                    _mongoDBConfig = Config.GetConfig<MongoDBConfig>();

                    if (_mongoDBConfig == null)
                    {
                        throw new Exception("缺少MongoDBConfig配置");
                    }
                    else
                    {
                        MongoClientSettings mongoSetting = new MongoClientSettings();
                        //设置连接超时时间  
                        mongoSetting.ConnectTimeout = new TimeSpan(_mongoDBConfig.TimeOut * TimeSpan.TicksPerSecond);
                        //设置数据库服务器  
                        mongoSetting.Server = new MongoServerAddress(_mongoDBConfig.Host, _mongoDBConfig.Port);
                        //创建Mongo的客户端  
                        MongoClient client = new MongoClient(mongoSetting); 
                        //得到服务器端并且生成数据库实例  
                        this._db = client.GetServer().GetDatabase(_mongoDBConfig.DBName); 
                    }
                }
            }
        }

        #region 插入
        /// <summary>  
        /// 将数据插入进数据库  
        /// </summary>  
        /// <typeparam name="T">需要插入数据库的实体类型</typeparam>  
        /// <param name="t">需要插入数据库的具体实体</param>  
        /// <param name="collectionName">指定插入的集合</param> 
        public bool Insert<T>(T t, string collectionName)
        {
            if (this._db == null)
            {
                return false;
            }
            try
            {
                MongoCollection<BsonDocument> mc = this._db.GetCollection<BsonDocument>(collectionName);
                //将实体转换为bson文档  
                BsonDocument bd = t.ToBsonDocument();
                //进行插入操作  
                WriteConcernResult result = mc.Insert(bd);
                if (string.IsNullOrWhiteSpace(result.ErrorMessage))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }  
        }

        /// <summary>  
        /// 批量插入数据  
        /// </summary>  
        /// <typeparam name="T">需要插入数据库的实体类型</typeparam>  
        /// <param name="list">需要插入数据的列表</param>  
        /// <param name="collectionName">指定要插入的集合</param>  
        public bool Insert<T>(List<T> list, string collectionName)
        {
            if (this._db == null)
            {
                return false;
            }
            try
            {
                MongoCollection<BsonDocument> mc = this._db.GetCollection<BsonDocument>(collectionName);
                //创建一个空间bson集合  
                List<BsonDocument> bsonList = new List<BsonDocument>();
                //批量将数据转为bson格式 并且放进bson文档  
                list.ForEach(t => bsonList.Add(t.ToBsonDocument()));
                //批量插入数据  
                mc.InsertBatch(bsonList);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }  
        #endregion

        #region 查询

        #endregion

        #region 查询
        /// <summary>  
        /// 查询一条记录
        /// </summary>  
        /// <typeparam name="T">该数据所属的类型</typeparam>  
        /// <param name="query">查询的条件 可以为空</param>  
        /// <param name="collectionName">去指定查询的集合</param>  
        /// <returns>返回一个实体类型</returns>  
        public T FindOne<T>(IMongoQuery query, string collectionName)
        {
            if (this._db == null)
            {
                return default(T);
            }
            try
            {
                MongoCollection<T> mc = this._db.GetCollection<T>(collectionName);
                query = this.InitQuery(query);
                T t = mc.FindOne(query);
                return t;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }  
        #endregion

        #region 删除
        /// <summary>  
        /// 移除指定的数据  
        /// </summary>  
        /// <typeparam name="T">移除的数据类型</typeparam>  
        /// <param name="query">移除的数据条件</param>  
        /// <param name="collectionName">指定的集合名词</param>  
        public bool Remove<T>(IMongoQuery query, string collectionName)
        {
            if (this._db == null)
            {
                return false;
            }
            try
            {
                MongoCollection<T> mc = this._db.GetCollection<T>(collectionName);
                query = this.InitQuery(query);
                //根据指定查询移除数据  
                mc.Remove(query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }  
        #endregion

        #region 私有辅助方法
        /// <summary>  
        /// 初始化查询记录 主要当该查询条件为空时 会附加一个恒真的查询条件，防止空查询报错  
        /// </summary>  
        /// <param name="query">查询的条件</param>  
        /// <returns></returns>  
        private IMongoQuery InitQuery(IMongoQuery query)
        {
            if (query == null)
            {
                //当查询为空时 附加恒真的条件 类似SQL：1=1的语法  
                query = Query.Exists(OBJECTID_KEY);
            }
            return query;
        }

        /// <summary>  
        /// 初始化排序条件  主要当条件为空时 会默认以ObjectId递增的一个排序  
        /// </summary>  
        /// <param name="sortBy"></param>  
        /// <param name="sortByName"></param>  
        /// <returns></returns>  
        private SortByDocument InitSortBy(SortByDocument sortBy, string sortByName)
        {
            if (sortBy == null)
            {
                sortBy = new SortByDocument(sortByName, -1);
            }
            return sortBy;
        }

        private UpdateDocument InitUpdateDocument(UpdateDocument update, string indexName)
        {
            if (update == null)
            {
                update = new UpdateDocument("$inc", new QueryDocument(indexName, 0));
            }
            return update;
        }
        #endregion

        public IMongoQuery file(FilterCondition filterCondition)
        {
            IMongoQuery query = null;

            foreach (var item in filterCondition.Fields)
            {
                if (item.FilterType.Equals(1))
                {
                    Query.EQ("","");
                }

            }

        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
        }
    }
}
