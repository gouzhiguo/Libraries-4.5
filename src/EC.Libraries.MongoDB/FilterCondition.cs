using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EC.Libraries.MongoDB
{
    /// <summary>
    /// 字段的查询过滤类型
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// 同时满足多个条件
        /// </summary>
        And = 1,
        /// <summary>
        /// 等于
        /// </summary>
        EQ = 2,
    }

    /// <summary>
    /// MongoDb查询分页过滤条件
    /// </summary>
    public class FilterCondition
    {
        private IList<FilterValue> fields = new List<FilterValue>();

        /// <summary>
        /// 不同过滤条件的过滤类型约定
        /// </summary>
        [Description("列的过滤类型")]
        public IList<FilterValue> Fields
        {
            get { return fields; } 
        }
    }

    public class FilterValue
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName{ set; get; }

        /// <summary>
        /// 字段值
        /// </summary>
        public object FieldValue { set; get; }

        /// <summary>
        /// 过滤类型
        /// </summary>
        public FilterType FilterType{ set; get; }
    }
}
