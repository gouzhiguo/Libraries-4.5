using System;
using EC.Libraries.Framework;
using EC.Libraries.MongoDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver.Builders;

namespace EC.Libraries.UnitTest
{
    /// <summary>
    /// http://blog.csdn.net/heyangyi_19940703/article/details/51192854
    /// http://blog.csdn.net/heyangyi_19940703/article/details/51198899
    /// </summary>
    [TestClass]
    public class MongoDBTest
    {
        [TestMethod]
        public void TestInsert()
        {
            using (var cliect = ClientProxy.GetInstance<IMongoDBProvider>())
            {
                var model = new Customer()
                {
                    Id = 1,
                    Account = "gouzhiguo",
                    CreatedDate = DateTime.Now
                };

                cliect.Insert<Customer>(model,"Customer");
            }
        }

        /// <summary>  
        /// 按条件查询一条数据  
        /// </summary>  
        [TestMethod]
        public void SelOne()
        {
            using (var cliect = ClientProxy.GetInstance<IMongoDBProvider>())
            {
                Query.EQ("Account", "gouzhiguo"),Query.EQ("","");
                var model = cliect.FindOne<Customer>(Query.And(Query.EQ("Account", "gouzhiguo")), "Customer");


                cliect.FindOne<Customer>(Query.And(Query.EQ("Account", "gouzhiguo"),Query.EQ("","")), "Customer");

            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
