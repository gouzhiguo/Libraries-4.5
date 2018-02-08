namespace EC.Libraries.File
{
    using EC.Libraries.Framework;

    /// <summary>
    /// 文件配置实体
    /// </summary>
    public class FileConfig : BaseConfig
    {
        /// <summary>
        /// 配置允许上传文件类型(白名单)
        /// </summary>
        public string AllowExtension { get; set; }

        /// <summary>
        /// 根目录
        /// </summary>
        public string RootDirectory { get; set; }
    }
}
