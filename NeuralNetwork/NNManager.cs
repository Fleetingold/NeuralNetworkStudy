using System;
using System.Collections.Generic;
using System.Drawing;

namespace NeuralNetwork
{
    public class NNManager
    {
        private int _numInputParameters;
        private int _numHiddenLayers;
        private int[] _hiddenNeurons;
        private int _numOutputParameters;
        private Network _network;
        private List<DataSet> _dataSets;

        /// <summary>
        /// 创建新网络
        /// </summary>
        /// <returns></returns>
        public NNManager SetupNetwork()
        {
            _numInputParameters = 2;

            int[] hidden = new int[2];
            hidden[0] = 3;
            hidden[1] = 1;
            _numHiddenLayers = 1;
            _hiddenNeurons = hidden;
            _numOutputParameters = 1;
            _network = new Network(_numInputParameters, _hiddenNeurons, _numOutputParameters);
            return this;
        }

        /// <summary>
        /// 手动输入数据
        /// </summary>
        /// <returns></returns>
        public NNManager GetTrainingDataFromUser()
        {
            var numDataSets = GetInput("\tHow many datasets are you going to enter?",1 , int.MaxValue);

            var newDataSets = new List<DataSet>();
            for (var i = 0; i < numDataSets; i++)
            {
                var values = GetInputData($"\tData Set {i + 1}: ");
                if (values == null)
                {
                    return this;
                }

                var expextedResult = GetExpectedResult($"\tExpected Result for Data Set {i + 1}: ");
                if (expextedResult == null)
                {
                    return this;
                }

                newDataSets.Add(new DataSet(values, expextedResult));
            }

            _dataSets = newDataSets;
            return this;
        }

        private double[] GetExpectedResult(string info)
        {
            return GetInputData(info);
        }

        private double[] GetInputData(string info)
        {
            Console.Write(info);
            List<double> result = new List<double>();

            do
            {
                try
                {
                    //将根据提示输入的数字字符串转换成int型   
                    //Console.ReadLine(),这个函数，是以回车判断字符串结束的  

                    string s = Console.ReadLine();
                    string[] ds = s.Split(" ");
                    foreach (string str in ds)
                    {
                        double number = Convert.ToDouble(str);
                        result.Add(number);
                    }
                    return result.ToArray();
                }
                catch
                {
                    Console.WriteLine("输入有误，重新输入！");
                }
            }
            while (true);
        }

        private int GetInput(string info, int minValue, int maxValue)
        {
            Console.Write(info);
            int number = 0;
            do
            {
                try
                {
                    //将根据提示输入的数字字符串转换成int型   
                    //Console.ReadLine(),这个函数，是以回车判断字符串结束的  

                    number = Convert.ToInt32(Console.ReadLine());//与下面的效果一样  
                    //number = System.Int32.Parse(Console.ReadLine());
                    return number;
                }
                catch
                {
                    Console.WriteLine("输入有误，重新输入！");
                }
            }
            while (true);
        }

        /// <summary>
        /// 导出网络
        /// </summary>
        /// <returns></returns>
        public NNManager ExportNetwork()
        {
            Console.WriteLine("\tExporting Network...");
            ExportHelper.ExportNetwork(_network);
            Console.WriteLine("\t**Exporting Complete!**", ConsoleColor.Green);
            return this;
        }

        /// <summary>
        /// 最小误差值法
        /// </summary>
        /// <returns></returns>
        public NNManager TrainNetworkToMininum()
        {
            var minError = GetDouble("\tMininum Error: ", 0.000000001, 1.0);
            Console.WriteLine("\tTrainning...");
            _network.Train(_dataSets, (int)minError);
            Console.WriteLine("\t**Training Complete**", ConsoleColor.Green);
            return this;
        }

        private double GetDouble(string info, double minError, double maxError)
        {
            Console.Write(info);
            double number = 0;
            do
            {
                try
                {
                    //将根据提示输入的数字字符串转换成int型   
                    //Console.ReadLine(),这个函数，是以回车判断字符串结束的  

                    number = Convert.ToDouble(Console.ReadLine());//与下面的效果一样  
                    //number = System.Int32.Parse(Console.ReadLine());
                    return number;
                }
                catch
                {
                    Console.WriteLine("输入有误，重新输入！");
                }
            }
            while (true);
        }

        /// <summary>
        /// 最大误差值法
        /// </summary>
        /// <returns></returns>
        public NNManager TrainNetworkToMaxinum()
        {
            int? maxEpoch = GetInput("\tMax Epoch: ", 1, int.MaxValue);
            if (!maxEpoch.HasValue)
            {
                return this;
            }

            Console.WriteLine("\tTrainning...");
            _network.Train(_dataSets, maxEpoch.Value);
            Console.WriteLine("\t**Training Complete**", Color.Green);
            return this;
        }

        /// <summary>
        /// 测试网络
        /// </summary>
        /// <returns></returns>
        public NNManager TestNetwork()
        {
            Console.WriteLine("\tTesting Network", Color.Green);
            Console.WriteLine("-------------------------------------------------", ConsoleColor.Green);
            while (true)
            {
                //从用户获取输入数据:
                var values = GetInputData($"\tType{_numInputParameters} inputs: ");
                if (values == null)
                {
                    return this;
                }

                //进行计算:
                var results = _network?.Compute(values);

                //打印出结果:
                foreach (var result in results)
                {
                    Console.WriteLine("\tOutput: " + result.ToString(), Color.Aqua);
                }
                return this;
            }
        }

        public NNManager ExportDatasets()
        {
            Console.WriteLine("\tExporting Datasets...", Color.Green);
            ExportHelper.ExportDatasets(_dataSets);
            Console.WriteLine("\t**Exporting Complete!**", Color.Green);
            return this;
        }
    }

    public class DataSet
    {
        public double[] Values { get; set; }
        public double[] Targets { get; set; }

        public DataSet(double[] values, double[] targets)
        {
            this.Values = values;
            this.Targets = targets;
        }
    }
}
