using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSimilar
{
    public class ReadFile
    {
        /// <summary>
                /// 读取CSV文件通过文本格式
                /// </summary>
                /// <param name="strpath"></param>
                /// <returns></returns>
        public static DataTable readCsvTxt(string strpath)
        {
            int intColCount = 0;
            bool blnFlag = true;
            DataTable mydt = new DataTable("myTableName");

            DataColumn mydc;
            DataRow mydr;

            string strline;
            string[] aryline;

            System.IO.StreamReader mysr = new System.IO.StreamReader(strpath);

            while ((strline = mysr.ReadLine()) != null)
            {
                aryline = strline.Split(',');

                if (blnFlag)
                {
                    blnFlag = false;
                    intColCount = aryline.Length;
                    for (int i = 0; i < aryline.Length; i++)
                    {
                        mydc = new DataColumn(aryline[i]);
                        mydt.Columns.Add(mydc);
                    }
                }

                mydr = mydt.NewRow();
                for (int i = 0; i < intColCount; i++)
                {
                    mydr[i] = aryline[i];
                }
                mydt.Rows.Add(mydr);
            }

            return mydt;
        }
    }
}
