using System;
using System.Collections.Generic;
using System.Text;

namespace AutoEncoder
{
    public struct RestrictedBoltzmannMachineLearningRate
    {
        internal double weights;
        internal double biases;
        internal double momentumWeights;
        internal double momentumBiases;

        public RestrictedBoltzmannMachineLearningRate(double weights, double biases, double momentumWeights, double momentumBiases) : this()
        {
            this.weights = weights;
            this.biases = biases;
            this.momentumWeights = momentumWeights;
            this.momentumBiases = momentumBiases;
        }
    }
}
