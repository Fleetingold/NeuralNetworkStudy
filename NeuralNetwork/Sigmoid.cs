using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public static class Sigmoid
    {
        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Output(double x)
        {
            return x < -45.0 ? 0.0 : x > 45.0 ? 1.0 : 1.0 / (1.0 + Math.Exp(-x));
        }

        /// <summary>
        /// 求导
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Derivative(double x)
        {
            return x * (1 - x);
        }
    }
}
