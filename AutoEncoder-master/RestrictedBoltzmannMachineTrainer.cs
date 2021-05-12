using System;
using System.Collections.Generic;
using System.Text;

namespace AutoEncoder
{
    public static class RestrictedBoltzmannMachineTrainer
    {
        private static RestrictedBoltzmannMachineLearningRate learnrate;
        private static RestrictedBoltzmannMachineWeightSet weightset;

        public static void Train(RestrictedBoltzmannMachineLayer layer, RestrictedBoltzmannMachineLayer hidderLayer, TrainingData trainingData, RestrictedBoltzmannMachineLearningRate learnrate,double weightSet)
        {

        }

        private static void TrainWeight(int PWhichVis, int PWhichHid, double PTrainAmount)
        {

        }

        private static void TrainBias(RestrictedBoltzmannMachineLayer PLayer, int PWhich, double PPosPhse, double PNegPhase)
        {

        }

        private static double CalcuteTrainAmount(double PPosVis, double PPosHid, double PNegVis, double PNegHid)
        {
            return ((PPosVis * PPosHid) - (PNegVis * PNegHid));
        }
    }
}
