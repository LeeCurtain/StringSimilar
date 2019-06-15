using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace StringSimilar
{
    class Program
    {
        static void Main(string[] args)
        {
            //编辑距离算法
            Similar similar = TextContrast.Instance.GetStringSimilarityPerZw("上海市浦东新区祖冲之路2277弄7号楼", "浦东新区祖冲之路2277弄");
            Similar similar1 = TextContrast.Instance.GetStringSimilarityPerYw("910 42TH STREET 1FL BROOKLYN NEW YORK", "910 42th St 1fl BROOKLYN New York");
            Similar similar2 = TextContrast.Instance.GetStringSimilarityPerYw("Level 13, 2-26 Park Street, SYDNEY NSW 2000：".ToUpper(), "Level 13, 2-26 Park Street, SYDNEY NSW 2000".ToUpper());
            //余弦算法
            double dou = Cosin.Instance.getSimilarity("最近由于工作项目，需要判断两个txt文本是否相似，于是开始在网上找资料研究，因为在程序中会把文本转换成String再做比较，所以最开始找到了这篇关于 距离编辑算法 Blog写的非常好，受益匪浅。于是我决定把它用到项目中，来判断两个文本的相似度。但后来实际操作发现有一些问题：直接说就是查询一本书中的相似章节花了我7、8分钟；这是我不能接受……于是停下来仔细分析发现，这种算法在此项目中不是特别适用，由于要判断一本书中是否有相同章节，所以每两个章节之间都要比较，若一本书书有x章的话，这里需对比x(x - 1) / 2次；而此算法采用矩阵的方式，计算两个字符串之间的变化步骤，会遍历两个文本中的每一个字符两两比较，可以推断出时间复杂度至少为document1.length × document2.length，我所比较的章节字数平均在几千～一万字；这样计算实在要了老命。",
                "最近由于工作项目，需要判断两个txt文本是否相似，于是开始在网上找资料研究，因为在程序中会把文本转换成String再做比较，所以最开始找到了这篇关于 距离编辑算法 Blog写的非常好，受益匪浅。于是我决定把它用到项目中，来判断两个文本的相似度。但后来实际操作发现有一些问题：直接说就是查询一本书中的相似章节花了我7、8分钟；这是我不能接受……于是停下来仔细分析发现，这种算法在此项目中不是特别适用，由于要判断一本书中是否有相同章节，所以每两个章节之间都要比较，若一本书书有x章的话，这里需对比x(x - 1) / 2次；而此算法采用矩阵的方式，计算两个字符串之间的变化步骤，，可以推断出时间复杂度至少为docuent1.length × document2.length，我所比较的章节字数平均～一万字；这样计算实在要老命。");//余弦算法相似度
            double dou1 = Cosin.Instance.getEnglishlarity("Level 13, 2-26 Park Street, SYDNEY NSW 2000：".ToUpper(), "Level 13, 2-26 Park Street, SYDNEY NSW 2000：".ToUpper());
            double dou2 = Cosin.Instance.getEnglishlarity("910 42TH STREET 1FL BROOKLYN NEW YORK".ToUpper(), "910 42th St 1fl BROOKLYN New York".ToUpper());
            double dou3 = Cosin.Instance.getSimilarity("上海市浦东新区祖冲之路2277弄7号楼", "浦东新区祖冲之路2277弄");
            Console.WriteLine(JsonConvert.SerializeObject(similar));
            Console.WriteLine(JsonConvert.SerializeObject(similar1));
            Console.WriteLine(JsonConvert.SerializeObject(similar2));
            Console.WriteLine(JsonConvert.SerializeObject(dou));
            Console.WriteLine(JsonConvert.SerializeObject(dou1));
            Console.WriteLine(JsonConvert.SerializeObject(dou2));
            Console.WriteLine(JsonConvert.SerializeObject(dou3));
            double y;

            double t = LevenshteinDistance.Instance.LevenshteinDistances("Level 13, 2-26 Park Street, SYDNEY NSW 2000", "Level 13, 2-26 Park Street, SYDNEY NSW 2000", out y);
            Console.WriteLine(y);
            Console.WriteLine(t);
            //测试
            string line;
            int i = 0;
            string str = AppDomain.CurrentDomain.BaseDirectory + @"\address.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(str);
            List<string> list = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                list.Add(line);
                i++;
            }
            file.Close();
            // var data = ReadFile.readCsvTxt(str);
            while (true)
            {
                Console.WriteLine("请输入中文地址：");
                string chinaArdess = Console.ReadLine();
                Console.WriteLine("请输入英文地址：");
                string englishArdess = Console.ReadLine();
                if (string.IsNullOrEmpty(chinaArdess))
                    chinaArdess = "九龍光道8號豪坊8樓E室";
                if (string.IsNullOrEmpty(englishArdess))
                    englishArdess = "Level 13, 2-26 Park Street, SYDNEY NSW 2000";
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Dictionary<string, double> dicChina = new Dictionary<string, double>();
                Dictionary<string, double> dicEngish = new Dictionary<string, double>();
                int j = 0;
                foreach (string dr in list)
                {
                    //if (dr== "Level 13, 2-26 Park Street, SYDNEY NSW 2000")
                    //{
                    char[] chary = new char[] { ',', '\'', '/', '-', '(', ')', '.', '，', '&', '?', '#' };
                    string address = dr;
                    foreach (char item in chary)
                    {
                        address = address.Replace(item.ToString(), "");
                    }
                    string number = Regex.Replace(address, @"\s", "");
                    try
                    {
                        ///if (dicChina.Count<=5 && dicEngish.Count<=5)
                        //{
                            if (Cosin.Instance.isIntergerOrLetter(number))
                            {
                                if (dicEngish.Count <= 5)
                                {
                                    double x;
                                    LevenshteinDistance.Instance.LevenshteinDistances(dr, englishArdess, out x);
                                    if (x > 0.6)
                                    {
                                        dicEngish.Add(dr + j, x);
                                    }
                                }
                                // var res = TextContrast.GetStringSimilarityPerYw(dr.ToUpper(), "Level 13, 2-26 Park Street, SYDNEY NSW 2000".ToUpper());//Level 13, 2-26 Park Street, SYDNEY NSW 2000
                                //if(res.precent>50)
                                //Console.WriteLine($"英文：{dr}：" + (JsonConvert.SerializeObject(x)));
                            }
                            else
                            {
                                if (dicChina.Count <= 5)
                                {
                                    double res = Cosin.Instance.getSimilarity(WordsHelper.ToSimplifiedChinese(address), WordsHelper.ToSimplifiedChinese(chinaArdess));
                                    if (res > 0.6)
                                    {
                                        dicChina.Add(dr + j, res);
                                    }
                                }
                                // Console.WriteLine($"中文：{dr}：简体中文：{WordsHelper.ToSimplifiedChinese(dr)}：" + res);
                            }
                            j++;
                        }                       
                    //}
                    catch (Exception e)
                    {

                        Console.WriteLine(e.Message);
                    }
                    // }
                }
                Console.WriteLine("输入的中文地址：" + chinaArdess);
                //Console.WriteLine("相似度前五的地址：");
                foreach (var item in dicChina.OrderByDescending(a => a.Value).Take(5))
                {
                    Console.WriteLine($"比对地址：{item.Key.ToString().Substring(0, item.Key.ToString().Length - 1)} ：比对的结果为：{item.Value}");
                }
                Console.WriteLine("输入的英文地址：" + englishArdess);
                //Console.WriteLine("相似度前五的地址：");
                foreach (var item in dicEngish.Where(a=>a.Value!=1).OrderByDescending(a => a.Value).Take(5))
                {
                    Console.WriteLine($"比对地址：{item.Key.ToString().Substring(0, item.Key.ToString().Length - 1)}：比对的结果为：{item.Value}");
                }

                sw.Stop();
                Console.WriteLine("总的比对数据条数："+i);
                Console.WriteLine("测量实例得出的总运行时间（毫秒为单位）：" + sw.ElapsedMilliseconds);
            }
        }
    }
}
