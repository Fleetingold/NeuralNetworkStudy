using System;

namespace AutoEncoder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            AutoencoderBuilder builder = new AutoencoderBuilder();
            builder.AddBinaryLayer(4);
            builder.AddBinaryLayer(3);
            builder.AddGaussianLayer(3);
            builder.AddGaussianLayer(1);

            builder.SetFineTuningLearningRateBiases(0, 1.0);
            builder.SetFineTuningLearningRateWeights(0, 1.0);
            builder.SetPreTrainingLearningRateBiases(0, 1.0);
            builder.SetPreTrainingLearningRateWeights(0, 1.0);
            builder.SetPreTrainingMomentumBiases(0, 0.1);
            builder.SetPreTrainingMomentumWeights(0, .05);

            Autoencoder encoder = builder.Build();

            RestrictedBoltzmannMachineLayer layer = encoder.GetLayer(0);
            RestrictedBoltzmannMachineLayer layerHidden = encoder.GetLayer(1);

            encoder.PreTrain(0, new double[] { 0.1, .05, .03, 0.8 });
            encoder.PreTrain(1, new double[] { 0.1, .05, .03, 0.9 });
            encoder.PreTrain(2, new double[] { 0.1, .05, .03, 0.1 });

            encoder.PreTrainComplete();

            encoder.Save("testencoder.txt");

            Autoencoder newAutoencoder = Autoencoder.Load("testencoder.txt");

            Console.ReadKey();
        }
    }
}
