using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoEncoder
{
    public abstract class RestrictedBoltzmannMachineLayer
    {
        protected double[] state;
        protected double[] bias;
        protected double[] biasChange;
        protected double[] activity;
        protected int numNeurons = 0;
        public abstract void SetState(int PWhich, double PInput);
        public abstract object Clone();

        public int Count { get { return numNeurons; } }

        public void SetBias(int index, double value)
        {
            bias[index] = value;
        }

        public void Save(TextWriter file)
        {
            throw new NotImplementedException();
        }

        public void Load(TextReader file)
        {
            throw new NotImplementedException();
        }
    }
}
