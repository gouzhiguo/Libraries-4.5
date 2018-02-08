using EC.Libraries.Framework;

namespace EC.Libraries.File
{
    using EC.Libraries.Framework;

    /// <summary>
    /// 对外公开文件上传接口
    /// </summary>
    public interface IFileProvider : IProxyBaseObject<IFileProvider>
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filePath">上传路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fileData">文件数据</param>
        UploadResponse<string> UploadFile(string filePath, string fileName, byte[] fileData);
    }
}
