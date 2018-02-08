namespace EC.Libraries.Util.ValidateCodes
{
    /// <summary>
    /// 公共验证码接口
    /// </summary>
    public interface  ICode
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="imageWidth">宽度</param>
        /// <param name="imageHeight">高度</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        CodeWrap CreateCode(int imageWidth, int imageHeight, int length=4);
    }
}
