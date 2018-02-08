using System;
using EC.Libraries.Framework;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace EC.Libraries.Lucene
{
    public class LuceneProvider
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private static object _lock = new object();

        /// <summary>
        /// Redis配置
        /// </summary>
        private static LuceneConfig _luceneConfig = null;

        /// <summary>
        /// 写索引对象
        /// </summary>
        private static IndexWriter _writer = null;

        /// <summary>
        /// 修改索引对象
        /// </summary>
        private static IndexModifier _modifier = null;

        /// <summary>
        /// 索引存放路径
        /// </summary>
        public static string path = "";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        public void Initialize(BaseConfig config = null)
        {
            lock (_lock)
            {
                if (config != null) _luceneConfig = config as LuceneConfig;

                if (_luceneConfig == null)
                {
                    _luceneConfig = Config.GetConfig<LuceneConfig>();

                    if (_luceneConfig == null) throw new Exception("缺少LuceneConfig配置");
                }
                path = _luceneConfig.Path;
            }
        }

        public void Add()
        {
            var modifier = new IndexModifier("", new PanGuAnalyzer(), false);

            try
            {
                var model = new ProductIndex();
                model.SysNo = 1;
                model.ProductName = "";
                model.Prices = "10";
                model.CreatedDate = DateTime.Now;

                var doc = new Document();

                Field field;

                field = new Field("SysNo", model.SysNo.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
                field.SetBoost(2.0F);
                doc.Add(field);

                field = new Field("ProductName", model.ProductName ?? "", Field.Store.YES, Field.Index.ANALYZED);
                field.SetBoost(2.0F);
                doc.Add(field);

                field = new Field("Prices", model.Prices, Field.Store.YES, Field.Index.NOT_ANALYZED);
                doc.Add(field);

                field = new Field("CreatedDate", model.CreatedDate.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
                doc.Add(field);

                _modifier.AddDocument(doc);
            }
            catch (Exception)
            {

            }
            finally
            {
                modifier.Flush();
                modifier.Close();
            }
        }
    }
}
