using System;
using System.IO;

namespace EC.Libraries.Util
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    /// <remarks>2013-08-13 苟治国 创建</remarks>
    public static class FileUtil
    {
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>存在:true  不存在:false</returns>
        /// <remarks>2013-12-30 苟治国 创建</remarks>
        public static Boolean HasFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return false;
            return File.Exists(filePath);
        }
    }
}
