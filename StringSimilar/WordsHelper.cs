using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringSimilar
{
    public static class WordsHelper
    {

        #region 字符串 转成 脏词检测字符串
        /// <summary>
        /// 转成 侦测字符串
        /// 1、转小写;2、全角转半角; 3、相似文字修改；4、繁体转简体
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToSenseIllegalWords(string s)
        {
            StringBuilder ts = new StringBuilder(s);
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (c < 'A') { }
                else if (c <= 'Z')
                {
                    ts[i] = (char)(c | 0x20);
                }
                else if (c < 9450) { }
                else if (c <= 12840)
                {//处理数字 
                    var index = Dict.nums1.IndexOf(c);
                    if (index > -1) { ts[i] = Dict.nums2[index]; }
                }
                else if (c == 12288)
                {
                    ts[i] = ' ';
                }
                else if (c < 0x4e00) { }
                else if (c <= 0x9fa5)
                {
                    var k = Dict.Simplified[c - 0x4e00];
                    if (k != c)
                    {
                        ts[i] = k;
                    }
                }
                else if (c < 65280) { }
                else if (c < 65375)
                {
                    var k = (c - 65248);
                    if ('A' <= k && k <= 'Z') { k = k | 0x20; }
                    ts[i] = (char)k;
                }
            }
            return ts.ToString();
        }
        #endregion
        internal static string RemoveNontext(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                bool remove = true;

                if (c == ' ')
                {
                    remove = false;
                }
                else if (c < 2)
                {
                    remove = false;
                }
                else if (c < '0') { }
                else if (c <= '9')
                {
                    remove = false;
                }
                else if (c < 'a') { }
                else if (c <= 'z')
                {
                    remove = false;
                }
                else if (c < 0x4e00) { }
                else if (c <= 0x9fa5)
                {
                    remove = false;
                }
                if (remove)
                {
                    sb[i] = (char)1;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 判断输入是否为中文  ,中文字符集为[0x4E00,0x9FA5]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool HasChinese(string content)
        {
            if (Regex.IsMatch(content, @"[\u4e00-\u9fa5]"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断输入是否全为中文,中文字符集为[0x4E00,0x9FA5]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool IsAllChinese(string content)
        {
            if (Regex.IsMatch(content, @"^[\u4e00-\u9fa5]*$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 转繁体中文
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToTraditionalChinese(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c >= 0x4e00 && c <= 0x9fa5)
                {
                    var k = Dict.Traditional[c - 0x4e00];
                    if (k != c)
                    {
                        sb[i] = k;
                    }
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 转简体中文
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToSimplifiedChinese(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c >= 0x4e00 && c <= 0x9fa5)
                {
                    var k = Dict.Simplified[c - 0x4e00];
                    if (k != c)
                    {
                        sb[i] = k;
                    }
                }
            }
            return sb.ToString();
        }




    }
}
