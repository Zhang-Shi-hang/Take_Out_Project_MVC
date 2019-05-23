using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace Take_Out_Project_MVC
{
    public class QRCode
    {

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="strContent">二维码包含的内容</param>
        /// <param name="strSaveImgPath">保存的图片路径，图片文件为png格式</param>
        /// <returns></returns>
        public static bool GetBarCode(string strContent, string strSaveImgPath)
        {
            bool bReturn = true;
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;

                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 7;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Drawing.Image myimg = qrCodeEncoder.Encode(strContent);

                myimg.Save(strSaveImgPath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                bReturn = false;
            }

            return bReturn;

        }

        /// <summary>
        /// 生成二维码(中间加LOGO标志)
        /// </summary>
        /// <param name="strContent">二维码包含的内容</param>
        /// <param name="strSaveImgPath">保存的图片路径，图片文件为png格式</param>
        /// <param name="strLogoImgPath">LOGO图片路径</param>
        public static bool GetBarCodeAddLogo(string strContent, string strSaveImgPath, string strLogoImgPath)
        {
            bool bReturn = true;
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;

                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 7;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Drawing.Image myimg = qrCodeEncoder.Encode(strContent);

                myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                System.IO.MemoryStream ms2 = new System.IO.MemoryStream();
                CombinImage(myimg, strLogoImgPath).Save(ms2, System.Drawing.Imaging.ImageFormat.Png);

                //保存为图片
                System.Drawing.Image _img = System.Drawing.Image.FromStream(ms2);
                _img.Save(strSaveImgPath);
            }
            catch (Exception ex)
            {
                bReturn = false;
            }

            return bReturn;
        }

        /// <summary>
        /// 调用此函数后使此两种图片合并，类似相册，有个 
        /// 背景图，中间贴自己的目标图片 
        /// </summary>
        /// <param name="imgBack">粘贴的源图片 </param>
        /// <param name="destImg">粘贴的目标图片 </param>
        /// <returns></returns>
        public static System.Drawing.Image CombinImage(System.Drawing.Image imgBack, string destImg)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(destImg); //照片图片 
                if (img.Height != 65 || img.Width != 65)
                {
                    img = KiResizeImage(img, 65, 65, 0);
                }
                Graphics g = Graphics.FromImage(imgBack);
                g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);
                //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高); 
                //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 -   img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框 
                //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高); 
                g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
                GC.Collect();
                return imgBack;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 改变图片大小
        /// </summary>
        /// <param name="bmp">原始Bitmap </param>
        /// <param name="newW">新的宽度 </param>
        /// <param name="newH">新的高度 </param>
        /// <param name="Mode"></param>
        /// <returns></returns>
        public static System.Drawing.Image KiResizeImage(System.Drawing.Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                System.Drawing.Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量 
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }

        }
    }
}
