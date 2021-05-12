using System;
using System.Collections.Generic;
using System.IO;

namespace AutoEncoder
{
    public class AutoencoderLearningRate
    {
        public List<double> preLearningRateBiases { get; set; }
        public List<double> preMomentumBiases { get; set; }
        public List<double> fineLearningRateBiases { get; set; }

        public List<double> preLearningRateWeights { get; set; }
        public List<double> preMomentumWeights { get; set; }
        public List<double> fineLearningRateWeights { get; set; }

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