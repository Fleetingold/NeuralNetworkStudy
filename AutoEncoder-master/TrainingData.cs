using System;
using System.Collections.Generic;
using System.Text;

namespace AutoEncoder
{
    public struct TrainingData
    {
        public double[] posVisible;
        public double[] posHidden;
        public double[] negVisible;
        public double[] negHidden;
    }
}
