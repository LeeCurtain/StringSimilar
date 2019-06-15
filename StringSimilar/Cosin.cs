
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSimilar
{
    public class Cosin
    {

        private static readonly Cosin _instance = null;
        public static Cosin Instance
        {
            get
            {
                if (_instance == null)
                {
                    return new Cosin();
                }
                return _instance;
            }
        }
        /// <summary>
        /// 中文对比
        /// </summary>
        /// <param name="doc1"></param>
        /// <param name="doc2"></param>
        /// <returns></returns>
        public  double getSimilarity(string doc1, string doc2)
        {
            if (doc1 != null && doc1.Trim().Length > 0 && doc2 != null
                    && doc2.Trim().Length > 0)
            {
                Dictionary<int, int[]> AlgorithmMap = new Dictionary<int, int[]>();
                //将两个字符串中的中文字符以及出现的总数封装到，AlgorithmMap中
                for (int i = 0; i < doc1.Length; i++)
                {
                    char d1 = doc1.ToCharArray()[i];
                    if (isHanZi(d1))
                    {
                        int charIndex = getGB2312Id(d1);
                        if (charIndex != -1)
                        {
                            int[] fq = null;
                            try
                            {
                                fq = AlgorithmMap[charIndex];
                            }
                            catch (Exception)
                            {
                            }
                            finally
                            {
                                if (fq != null && fq.Length == 2)
                                {
                                    fq[0]++;
                                }
                                else
                                {
                                    fq = new int[2];
                                    fq[0] = 1;
                                    fq[1] = 0;
                                    AlgorithmMap.Add(charIndex, fq);
                                }
                            }
                        }
                    }
                   
                }

                for (int i = 0; i < doc2.Length; i++)
                {
                    char d2 = doc2.ToCharArray()[i];
                    if (isHanZi(d2))
                    {
                        int charIndex = getGB2312Id(d2);
                        if (charIndex != -1)
                        {
                            int[] fq = null;
                            try
                            {
                                fq = AlgorithmMap[charIndex];
                            }
                            catch (Exception)
                            {
                            }
                            finally
                            {
                                if (fq != null && fq.Length == 2)
                                {
                                    fq[1]++;
                                }
                                else
                                {
                                    fq = new int[2];
                                    fq[0] = 0;
                                    fq[1] = 1;
                                    AlgorithmMap.Add(charIndex, fq);
                                }
                            }
                        }
                    }
                }

                double sqdoc1 = 0;
                double sqdoc2 = 0;
                double denominator = 0;
                foreach (KeyValuePair<int, int[]> par in AlgorithmMap)
                {
                    int[] c = par.Value;
                    denominator += c[0] * c[1];
                    sqdoc1 += c[0] * c[0];
                    sqdoc2 += c[1] * c[1];
                }
                return denominator / Math.Sqrt(sqdoc1 * sqdoc2);
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 英文对比
        /// </summary>
        /// <param name="doc1"></param>
        /// <param name="doc2"></param>
        /// <returns></returns>
        public  double getEnglishlarity(string doc1, string doc2)
        {
            if (doc1 != null && doc1.Trim().Length > 0 && doc2 != null
                    && doc2.Trim().Length > 0)
            {
                //依据空格分组
               // string[] doc1Arry = doc1.Split(' ');
                Dictionary<int, int[]> AlgorithmMap = new Dictionary<int, int[]>();
                //将两个字符串中de英文文字符以及出现的总数封装到，AlgorithmMap中
                for (int i = 0; i < doc1.Length; i++)
                {
                    char d1 = doc1.ToCharArray()[i];
                    if (IsNatural_Number(d1))
                    {
                        int charIndex = Asc(d1);
                        if (charIndex != -1)
                        {
                            int[] fq = null;
                            try
                            {
                                fq = AlgorithmMap[charIndex];
                            }
                            catch (Exception)
                            {
                            }
                            finally
                            {
                                if (fq != null && fq.Length == 2)
                                {
                                    fq[0]++;
                                }
                                else
                                {
                                    fq = new int[2];
                                    fq[0] = 1;
                                    fq[1] = 0;
                                    AlgorithmMap.Add(charIndex, fq);
                                }
                            }
                        }
                    }

                }

                for (int i = 0; i < doc2.Length; i++)
                {
                    char d2 = doc2.ToCharArray()[i];
                    if (IsNatural_Number(d2))
                    {
                        int charIndex = Asc(d2);
                        if (charIndex != -1)
                        {
                            int[] fq = null;
                            try
                            {
                                fq = AlgorithmMap[charIndex];
                            }
                            catch (Exception)
                            {
                            }
                            finally
                            {
                                if (fq != null && fq.Length == 2)
                                {
                                    fq[1]++;
                                }
                                else
                                {
                                    fq = new int[2];
                                    fq[0] = 0;
                                    fq[1] = 1;
                                    AlgorithmMap.Add(charIndex, fq);
                                }
                            }
                        }
                    }
                }

                double sqdoc1 = 0;
                double sqdoc2 = 0;
                double denominator = 0;
                foreach (KeyValuePair<int, int[]> par in AlgorithmMap)
                {
                    int[] c = par.Value;
                    denominator += c[0] * c[1];
                    sqdoc1 += c[0] * c[0];
                    sqdoc2 += c[1] * c[1];
                }
                return denominator / Math.Sqrt(sqdoc1 * sqdoc2);
            }
            else
            {
                throw new Exception();
            }
        }

        public  bool isHanZi(char ch)
        {
            // 判断是否汉字
            return (ch >= 0x4E00 && ch <= 0x9FA5);
        }

        public  bool IsNatural_Number(char str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return reg1.IsMatch(str.ToString());
        }
        public  bool isIntergerOrLetter(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return reg1.IsMatch(str);
        }


        /**
         * 根据输入的Unicode字符，获取它的GB2312编码或者ascii编码，
         * 
         * @param ch
         *            输入的GB2312中文字符或者ASCII字符(128个)
         * @return ch在GB2312中的位置，-1表示该字符不认识
         */
        public  short getGB2312Id(char ch)
        {
            try
            {
                byte[] buffer = System.Text.Encoding.GetEncoding("gb2312").GetBytes(ch.ToString());
                if (buffer.Length != 2)
                {
                    // 正常情况下buffer应该是两个字节，否则说明ch不属于GB2312编码，故返回'?'，此时说明不认识该字符
                    return -1;
                }
                int b0 = (int)(buffer[0] & 0x0FF) - 161; // 编码从A1开始，因此减去0xA1=161
                int b1 = (int)(buffer[1] & 0x0FF) - 161; // 第一个字符和最后一个字符没有汉字，因此每个区只收16*6-2=94个汉字
                return (short)(b0 * 94 + b1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }
        /// <summary>
        /// 英文
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public  short Asc(char character)
        {
            if (character.ToString().Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                short intAsciiCode = asciiEncoding.GetBytes(character.ToString())[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }
        }
    }
}
