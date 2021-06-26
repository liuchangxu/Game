using System;
using System.Collections.Generic;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("数据初始化...");
            //初始化数据源  三行 3，5，7
            Helper.Init();

            //打印数据
            PrintData();

            Console.WriteLine("加载完成" + "\r\n");
            Console.WriteLine("请输入任意键开始游戏");
            Console.ReadKey();

            Console.WriteLine("" + "\r\n");
            var round = 0;
            var loop = true;
            do
            {
                round++;
                Console.WriteLine($"======第{round}轮开始======" + "\r\n");
                for (int i = 1; i <= 2; i++)
                {
                    var resultData = Helper.DrawLots("玩家" + i);
                    var win = ProcessData(resultData, "玩家" + i);
                    if (win)
                    {
                        Console.WriteLine($"玩家{i}输了！");
                        loop = false;
                        break;
                    }
                    Console.WriteLine("");
                }
                if (loop)
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("剩余数据：" + "\r\n");
                    PrintData();
                }
                Console.WriteLine($"======第{round}轮结束======" + "\r\n");
                if (loop)
                {
                    Console.WriteLine("请输入任意键继续游戏");
                    Console.ReadKey();
                    Console.WriteLine("");
                }

            } while (loop);

            Console.WriteLine("游戏结束！按任意键结束程序");
            Console.ReadKey();
        }

        /// <summary>
        /// 打印数据源
        /// </summary>
        public static void PrintData()
        {
            if (Helper.dataSourceList.Count > 0)
            {
                var keyList = new List<int>();
                foreach (var key in Helper.dataSourceList.Keys)
                {
                    keyList.Add(key);
                }
                if (keyList.Count > 0)
                {
                    for (int i = 0; i < keyList.Count; i++)
                    {
                        Console.WriteLine($"第{keyList[i]}行数据：");
                        var dataList = Helper.dataSourceList[keyList[i]];
                        if (dataList.Count > 0)
                        {
                            for (int j = 0; j < dataList.Count; j++)
                            {
                                Console.Write(dataList[j] + "\t");
                            }
                            Console.WriteLine("");
                        }
                    }
                }
            }
        }

        public static bool ProcessData(Tuple<bool, int, List<int>, bool> data, string name)
        {
            if (data.Item1)
            {
                Console.WriteLine($"{name} 第{data.Item2}行数据：{data.Item3.Count}个");
                if (data.Item3 != null)
                {
                    for (int i = 0; i < data.Item3.Count; i++)
                    {
                        Console.Write(data.Item3[i] + "\t");
                    }
                    Console.WriteLine("");
                }
                return data.Item4;
            }
            else
            {
                Console.WriteLine($"{name} 第{data.Item2}行数据：异常错误");
            }
            return false;
        }

    }
}
