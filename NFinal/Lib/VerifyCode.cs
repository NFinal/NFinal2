using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace NFinal.Lib
{
    public class VerifyCode
    {
        /// <summary>
        /// 验证码校验
        /// </summary>
        public string vcheck = null;
        /// <summary>
        /// 验证码
        /// </summary>
        private string _vcode = null;
        /// <summary>
        /// md5验证salt
        /// </summary>
        private static string salt=null;
        public VerifyCode()
        {
            _vcode = GenerateCheckCode();
            if (salt == null)
            {
                salt = ""; //getCpu();
            }
            using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                vcheck = BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_vcode + salt))).Replace("-", "");
            }
        }
        /// <summary>
        /// 取得设备硬盘的序列号
        /// </summary>
        /// <returns></returns>
        //private static string GetDiskVolumeSerialNumber()
        //{
        //    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //    ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
        //    disk.Get();
        //    return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        //}
        /// <summary>
        /// 获得CPU的序列号
        /// </summary>
        /// <returns></returns>
        //private static string getCpu()
        //{
        //    string strCpu = null;
        //    ManagementClass myCpu = new ManagementClass("win32_Processor");
        //    ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
        //    foreach (ManagementObject myObject in myCpuConnection)
        //    {
        //        strCpu = myObject.Properties["ProcessorId"].Value.ToString();
        //        break;
        //    }
        //    return strCpu;
        //}
        private string GenerateCheckCode()
        {
            //创建整型型变量   
            int number;
            //创建字符型变量   
            char code;
            //创建字符串变量并初始化为空   
            string checkCode = String.Empty;
            //创建Random对象   
            Random random = new Random();
            //使用For循环生成4个数字   
            for (int i = 0; i < 4; i++)
            {
                //生成一个随机数   
                number = random.Next();
                //将数字转换成为字符型   
                code = (char)('0' + (char)(number % 10));

                checkCode += code.ToString();
            }
            //返回字符串   
            return checkCode;
        }
        private System.Drawing.Image CreateCheckCodeImage(string checkCode)
        {
            //判断字符串不等于空和null   
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return null;
            //创建一个位图对象   
            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            //创建Graphics对象   
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);

            try
            {
                //生成随机生成器   
                Random random = new Random();

                //清空图片背景色   
                g.Clear(System.Drawing.Color.White);

                //画图片的背景噪音线   
                for (int i = 0; i < 2; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Black), x1, y1, x2, y2);
                }

                System.Drawing.Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                    System.Drawing.Color.Blue,
                    System.Drawing.Color.DarkRed,
                    1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点   
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, System.Drawing.Color.FromArgb(random.Next()));
                }

                //画图片的边框线   
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            }
            finally
            {
                g.Dispose();
            }
            return image;
        }
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <returns></returns>
        public byte[] GetGif()
        {
            //将图片输出到页面上   
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.Drawing.Image image = CreateCheckCodeImage(_vcode);
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            try
            {
                byte[] buffer= ms.ToArray();
                return buffer;
            }
            finally
            {
                ms.Dispose();
                image.Dispose();
            }
        }
        /// <summary>
        /// 验证码验证
        /// </summary>
        /// <param name="vcheck">验证码校验字段，存于Cookie.vcheck中</param>
        /// <param name="vcode">验证码</param>
        /// <returns></returns>
        public bool Check(string vcheck,string vcode)
        {
            using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                if (BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(vcode + salt))).Replace("-", "") == vcheck)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
