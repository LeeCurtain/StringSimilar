using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSimilar
{
    public class LevenshteinDistance
    {


        private static LevenshteinDistance _instance = null;
        public static LevenshteinDistance Instance
        {
            get
            {
                if (_instance == null)
                {
                    return new LevenshteinDistance();
                }
                return _instance;
            }
        }


        /// <summary>
            /// 取最小的一位数
            /// </summary>
            /// <param name="first"></param>
            /// <param name="second"></param>
            /// <param name="third"></param>
            /// <returns></returns>
        public int LowerOfThree(int first, int second, int third)
        {
            int min = first;
            if (second < min)
                min = second;
            if (third < min)
                min = third;
            return min;
        }

        public int Levenshtein_Distance(string str1, string str2)
        {
            int[,] Matrix;
            int n = str1.Length;
            int m = str2.Length;

            int temp = 0;
            char ch1;
            char ch2;
            int i = 0;
            int j = 0;
            if (n == 0)
            {
                return m;
            }
            if (m == 0)
            {

                return n;
            }
            Matrix = new int[n , m];

            for (i = 0; i <= n; i++)
            {
                //初始化第一列
                Matrix[i, 0] = i;
            }

            for (j = 0; j <= m; j++)
            {
                //初始化第一行
                Matrix[0, j] = j;
            }

            for (i = 1; i <= n; i++)
            {
                ch1 = str1[i - 1];
                for (j = 1; j <= m; j++)
                {
                    ch2 = str2[j - 1];
                    if (ch1.Equals(ch2))
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = 1;
                    }
                    Matrix[i, j] = LowerOfThree(Matrix[i - 1, j], Matrix[i, j - 1], Matrix[i - 1, j - 1]);


                }
            }

            for (i = 0; i <= n; i++)
            {
                for (j = 0; j <= m; j++)
                {
                    Console.Write(" {0} ", Matrix[i, j]);
                }
                Console.WriteLine("");
            }
            return Matrix[n, m];

        }

        /// <summary>
            /// 计算字符串相似度
            /// </summary>
            /// <param name="str1"></param>
            /// <param name="str2"></param>
            /// <returns></returns>
        public decimal LevenshteinDistancePercent(string str1, string str2)
        {
            int maxLenth = str1.Length > str2.Length ? str1.Length : str2.Length;
            int val = Levenshtein_Distance(str1, str2);
            return 1 - (decimal)val / maxLenth;
        }

        /// <summary>
        /// 编辑距离（Levenshtein Distance）
        /// </summary>
        /// <param name="source">源串</param>
        /// <param name="target">目标串</param>
        /// <param name="similarity">输出：相似度，值在0～１</param>
        /// <param name="isCaseSensitive">是否大小写敏感</param>
        /// <returns>源串和目标串之间的编辑距离</returns>
        public  Int32 LevenshteinDistances(String source, String target, out Double similarity, Boolean isCaseSensitive = false)
        {
            if (String.IsNullOrEmpty(source))
            {
                if (String.IsNullOrEmpty(target))
                {
                    similarity = 1;
                    return 0;
                }
                else
                {
                    similarity = 0;
                    return target.Length;
                }
            }
            else if (String.IsNullOrEmpty(target))
            {
                similarity = 0;
                return source.Length;
            }

            String From, To;
            if (isCaseSensitive)
            {   // 大小写敏感
                From = source;
                To = target;
            }
            else
            {   // 大小写无关
                From = source.ToLower();
                To = target.ToLower();
            }

            // 初始化
            Int32 m = From.Length;
            Int32 n = To.Length;
            Int32[,] H = new Int32[m + 1, n + 1];
            for (Int32 i = 0; i <= m; i++) H[i, 0] = i;  // 注意：初始化[0,0]
            for (Int32 j = 1; j <= n; j++) H[0, j] = j;

            // 迭代
            for (Int32 i = 1; i <= m; i++)
            {
                Char SI = From[i - 1];
                for (Int32 j = 1; j <= n; j++)
                {   // 删除（deletion） 插入（insertion） 替换（substitution）
                    if (SI == To[j - 1])
                        H[i, j] = H[i - 1, j - 1];
                    else
                        H[i, j] = Math.Min(H[i - 1, j - 1], Math.Min(H[i - 1, j], H[i, j - 1])) + 1;
                }
            }

            // 计算相似度
            Int32 MaxLength = Math.Max(m, n);   // 两字符串的最大长度
            similarity = ((Double)(MaxLength - H[m, n])) / MaxLength;

            return H[m, n];    // 编辑距离
        }

    }
}
