using System.Text;
using System.Security.Cryptography;

namespace Alipay
{
    internal sealed class MD5
    {
        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="str">需要签名的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="charset">编码格式</param>
        /// <returns>签名结果</returns>
        internal static string Sign(string str, string key, string charset)
        {
            var sb = new StringBuilder(32);
            str += key;
            var md5 = new MD5CryptoServiceProvider();
            var t = md5.ComputeHash(Encoding.GetEncoding(charset).GetBytes(str));
            for (var i = 0; i < t.Length; i++)
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            return sb.ToString();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="sign">签名结果</param>
        /// <param name="key">密钥</param>
        /// <param name="charset">编码格式</param>
        /// <returns>验证结果</returns>
        internal static bool Verify(string prestr, string sign, string key, string charset)
        {
            var mysign = Sign(prestr, key, charset);
            return mysign == sign;
        }
    }
}