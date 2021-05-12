using System;
using System.Collections.Generic;
using System.Text;

namespace AutoEncoder
{
    public class AutoencoderBuilder
    {
        private List<RestrictedBoltzmannMachineLayer> layers = new List<RestrictedBoltzmannMachineLayer>();
        private AutoencoderLearningRate learnrate = new AutoencoderLearningRate();
        private IWeightInitializer weightinitializer = new GaussianWeightInitializer();

        private void AddLayer(RestrictedBoltzmannMachineLayer PLayer)
        {
            learnrate.preLearningRateBiases.Add(0.001);
            learnrate.preMomentumBiases.Add(0.5);
            learnrate.fineLearningRateBiases.Add(0.001);
            if (layers.Count >= 1)
            {
                learnrate.preLearningRateWeights.Add(0.001);
                learnrate.preMomentumWeights.Add(0.5);
                learnrate.fineLearningRateWeights.Add(0.001);
            }
            layers.Add(PLayer);
        }

        public void SetPreTrainingLearningRateWeights(int PWhich, double PLR)
        {
            learnrate.preLearningRateWeights[PWhich] = PLR;
        }

        public void SetPreTrainingLearningRateBiases(int PWhich, double PLR)
        {
            learnrate.preLearningRateBiases[PWhich] = PLR;
        }

        public void SetPreTrainingMomentumWeights(int PWhich, double PMom)
        {
            learnrate.preMomentumWeights[PWhich] = PMom;
        }

        public void SetPreTrainingMomentumBiases(int PWhich, double PMom)
        {
            learnrate.preMomentumBiases[PWhich] = PMom;
        }

        public void SetFineTuningLearningRateWeights(int PWhich, double PLR)
        {
            learnrate.fineLearningRateWeights[PWhich] = PLR;
        }

        public void SetFineTuningLearningRateBiases(int PWhich, double PLR)
        {
            learnrate.fineLearningRateBiases[PWhich] = PLR;
        }

        public void AddBinaryLayer(int size)
        {
            AddLayer(new RestrictedBoltzmannMachineBinaryLayer(size));
        }

        public void AddGaussianLayer(int size)
        {
            AddLayer(new RestrictedBoltzmannMachineGaussianLayer(size));
        }

        public Autoencoder Build()
        {
            return new Autoencoder(layers, learnrate, weightinitializer);
        }
    }
}
