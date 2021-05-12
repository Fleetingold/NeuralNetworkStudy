using System;
using System.Collections.Generic;
using System.Text;

namespace AutoEncoder
{
    public class RestrictedBolzmannMachine
    {
        private RestrictedBoltzmannMachineLayer visibleLayers;
        private RestrictedBoltzmannMachineLayer hiddenLayers;
        private RestrictedBoltzmannMachineWeightSet weights;
        private RestrictedBoltzmannMachineLearningRate learnrate;
        private TrainingData trainingData;
        private int numVisibleLayers;
        private int numHiddenLayers;
    }
}
