using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;

namespace EC.Libraries.Util
{
    /// <summary>
    /// 合成图片
    /// </summary>
    public class CombinImageUtil
    {
        /// <summary>
        /// 会产生graphics异常的PixelFormat
        /// </summary>
        private static readonly PixelFormat[] IndexedPixelFormats = { 
            PixelFormat.Undefined, 
            PixelFormat.DontCare,
            PixelFormat.Format16bppArgb1555, 
            PixelFormat.Format1bppIndexed,
            PixelFormat.Format4bppIndexed,
            PixelFormat.Format8bppIndexed
        };

        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns>是否存在</returns>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            return IndexedPixelFormats.Contains(imgPixelFormat);
        }

        /// <summary>
        /// 通过链接获取Image对象
        /// </summary>
        /// <param name="strUrl">图片链接地址</param>
        /// <returns>Image对象</returns>
        public static Image UriConvertToImage(string strUrl)
        {
            Image myImage = null;
            var request = HttpWebRequest.Create(strUrl);
            using (var response = request.GetResponse())
            {
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    myImage = new Bitmap(stream);
                }
            }
            return myImage;
        }

        public static Image CombinImageByUir(string originalImageUri, string targetImageUri, int w, int h, int x, int y)
        {
            var originalImage = CombinImageUtil.UriConvertToImage(originalImageUri);
            var targetImage = CombinImageUtil.UriConvertToImage(targetImageUri);

            return null;
        }

        public static Image CombinImage(Image originalImage, Image targetImage, int w, int h, int x, int y)
        {
            if (targetImage.Width != w || targetImage.Height != h)
            {
                targetImage = ResizeImage(targetImage, w, h);
            }

            //重新定义画布
            Graphics g = Graphics.FromImage(originalImage);

            //canvas.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);
            //canvas.DrawImage(targetImage, x, y, targetImage.Width, targetImage.Height);

            originalImage.Save(@"C:\new.png", ImageFormat.Png);
            g.DrawImage(targetImage, -50, -50, 212, 203);
            GC.Collect();
            return originalImage;

        }

        /// <summary>
        /// 合并两种图片
        /// </summary>
        /// <param name="originalImagePath">源图的Image对象</param>
        /// <param name="targetImagePath">目标图的Image对象</param>
        /// <param name="w">目标图宽度</param>
        /// <param name="h">目标图高度</param>
        /// <param name="x">所绘图左上角的x坐标</param>
        /// <param name="y">所绘图左上角的y坐标</param>
        /// <returns>合成图Image对象</returns>
        public static Image CombinImage(string originalImagePath, string targetImagePath, int w, int h, int x, int y)
        {

            var originalImage = Image.FromFile(originalImagePath);        //背景图片
            var targetImage = Image.FromFile(targetImagePath);            //目标图片

            targetImage = ResizeImage(targetImage, w, h);

            //重新定义画布
            Graphics g = Graphics.FromImage(originalImage);

            //canvas.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);
            //canvas.DrawImage(targetImage, x, y, targetImage.Width, targetImage.Height);

            originalImage.Save(@"C:\new.png", ImageFormat.Png);
            g.DrawImage(targetImage, -50, -50, 212, 203);
            GC.Collect();
            return originalImage;
        }

        /// <summary>
        /// 创建Graphics
        /// </summary>
        /// <param name="img">图片的Image对象</param>
        /// <returns>Graphics</returns>
        private static Graphics ImageConvertToGraphics(Image img)
        {
            Graphics graphics = null;
            //如果原图片是索引像素格式之列的，则需要转换
            if (IsPixelFormatIndexed(img.PixelFormat))
            {
                var bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
                graphics = Graphics.FromImage(bmp);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
            }
            else //否则直接操作
            {
                graphics = Graphics.FromImage(img);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
            }
            return graphics;
        }

        /// <summary>    
        /// 重新设置图片尺寸    
        /// </summary>    
        /// <param name="originalImage">原始Bitmap</param>    
        /// <param name="w">新的宽度</param>    
        /// <param name="h">新的高度</param>    
        /// <returns>处理以后的图片</returns>
        public static Image ResizeImage(Image originalImage, int w, int h)
        {
            Image image = new Bitmap(w, h);

            var graphics = Graphics.FromImage(image);
            // 插值算法的质量    
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, new Rectangle(0, 0, w, h), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            graphics.Dispose();
            return image;
        }
    }
}
