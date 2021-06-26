using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Helper
    {
        public static Dictionary<int, List<int>> dataSourceList = new Dictionary<int, List<int>>();
        /// <summary>
        /// 初始化三行数据  分别是3，5，7
        /// </summary>
        public static void Init()
        {
            dataSourceList.Clear();

            for (int i = 1; i <= 3; i++)
            {
                var dataList = new List<int>();
                if (i == 1)
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        dataList.Add(j);
                    }
                    dataSourceList.Add(1, dataList);
                }
                if (i == 2)
                {
                    for (int j = 4; j <= 8; j++)
                    {
                        dataList.Add(j);
                    }
                    dataSourceList.Add(2, dataList);
                }
                if (i == 3)
                {
                    for (int j = 9; j <= 15; j++)
                    {
                        dataList.Add(j);
                    }
                    dataSourceList.Add(3, dataList);
                }

            }

        }

        /// <summary>
        /// 玩家随机取某一行若干个数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Tuple<bool, int, List<int>, bool> DrawLots(string name)
        {
            var dataList = new List<int>();
            var rowNumber = 0;
            var ok = false;
            var win = false;
            try
            {
                if (dataSourceList.Count > 0)
                {//如果数据源存在数据则执行
                    var rnd = new Random();
                    //key值集合
                    var keyList = new List<int>();
                    foreach (var key in dataSourceList.Keys)
                    {
                        keyList.Add(key);
                    }
                    if (keyList.Count > 0)
                    {
                        //随机key值索引
                        var keyIndex = rnd.Next(0, keyList.Count);
                        if (keyIndex >= 0)
                        {
                            var key = keyList[keyIndex];
                            if (key > 0)
                            {
                                //取行号对应的数据总个数
                                var rowDataList = dataSourceList.GetValueOrDefault(key);
                                if (rowDataList != null && rowDataList.Count > 0)
                                {//如果存在数据则执行
                                 //取行号对应的数据随机总个数
                                    var dataNumberCount = rnd.Next(1, rowDataList.Count + 1);
                                    if (dataNumberCount > 0)
                                    {
                                        for (int i = 0; i < dataNumberCount; i++)
                                        {
                                            dataList.Add(rowDataList[i]);
                                        }
                                        for (int j = 0; j < dataList.Count; j++)
                                        {//循环删除取出的数据
                                            rowDataList.Remove(dataList[j]);
                                        }
                                        if (rowDataList.Count == 0)
                                        {
                                            //如果行号对应的数据都取玩了，则删除行号
                                            dataSourceList.Remove(key);
                                        }
                                        rowNumber = key;
                                        ok = true;
                                        win = dataSourceList.Count == 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ok = false;
                rowNumber = 0;
                dataList = null;
                throw (e);
            }
            finally
            {

            }
            return new Tuple<bool, int, List<int>, bool>(ok, rowNumber, dataList, win);
        }
    }
}
