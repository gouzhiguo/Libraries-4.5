using System;
using System.IO;
using System.Web;
using EC.Libraries.Framework;

namespace EC.Libraries.File
{
    /// <summary>
    /// 文件上传提供类
    /// </summary>
    internal class FileProvide : IFileProvider
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private static object _lock = new object();

        /// <summary>
        /// File配置
        /// </summary>
        private static FileConfig _fileConfig = null;

        /// <summary>
        /// 获取所需的基础调用实体
        /// </summary>
        public IFileProvider Instance
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
                if (config != null) _fileConfig = config as FileConfig;

                if (_fileConfig == null)
                {
                    _fileConfig = Config.GetConfig<FileConfig>();

                    if (_fileConfig == null) throw new Exception("缺少FileConfig配置");
                }
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="folder">上传文件夹</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fileData">文件数据</param>
        public UploadResponse<string> UploadFile(string folder, string fileName, byte[] fileData)
        {
            var response = new UploadResponse<string>()
            {
                Status = false
            };
            try
            {
                var saveFolder = string.Format(@"{0}{1}\\{2}", HttpRuntime.AppDomainAppPath, _fileConfig.RootDirectory, folder);
                if (!saveFolder.EndsWith("\\")) saveFolder += "\\";
                if (!Directory.Exists(saveFolder))
                    Directory.CreateDirectory(saveFolder);
                var saveFullPath = Path.Combine(saveFolder, fileName);
                System.IO.File.WriteAllBytes(saveFullPath, fileData);

                response.Status = true;
                response.Data = string.Format("/{0}/{1}/{2}", _fileConfig.RootDirectory,folder, fileName);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
        }
    }
}
