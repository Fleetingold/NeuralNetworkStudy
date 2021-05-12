using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoEncoder
{
    public class Autoencoder
    {
        private int numlayers;
        private bool pretraining = true;
        private RestrictedBoltzmannMachineLayer[] layers;
        private AutoencoderLearningRate learnrate;
        private AutoencoderWeights recognitionweights;
        private AutoencoderWeights generateiveweights;
        private TrainingData[] trainingData;
        private List<IErrorObserver> errorobservers;

        private IWeightInitializer weightinitializer;

        public Autoencoder()
        {
            errorobservers = new List<IErrorObserver>();
        }

        public Autoencoder(List<RestrictedBoltzmannMachineLayer> layersList, AutoencoderLearningRate learnrate, IWeightInitializer weightinitializer) : this()
        {
            this.layers = layersList.ToArray();
            this.learnrate = learnrate;
            this.weightinitializer = weightinitializer;
        }

        private void InitializeBiases(IWeightInitializer PWInitializer)
        {
            for (int i = 0; i < numlayers; i++)
            {
                for (int j = 0; j < layers[i].Count; j++)
                {
                    layers[i].SetBias(j, PWInitializer.InitializeBias());
                }
            }
        }

        private void InitializeTrainingData()
        {
            trainingData = new TrainingData[numlayers - 1];
            for (int i = 0; i < numlayers - 1; i++)
            {
                trainingData[i].posVisible = new double[layers[i].Count];
                Utility.SetArrayToZero(trainingData[i].posVisible);
                trainingData[i].posHidden = new double[layers[i + 1].Count];
                Utility.SetArrayToZero(trainingData[i].posHidden);
                trainingData[i].negVisible = new double[layers[i].Count];
                Utility.SetArrayToZero(trainingData[i].negVisible);
                trainingData[i].negHidden = new double[layers[i + 1].Count];
                Utility.SetArrayToZero(trainingData[i].negHidden);
            }
        }

        public RestrictedBoltzmannMachineLayer GetLayer(int PWhichLayer)
        {
            Utility.WithinBounds("Layer index out of bounds!", PWhichLayer, numlayers);
            return layers[PWhichLayer];
        }

        public void PreTrain(int PPreSynapticLayer, double[] data)
        {
            PerformPreTraining(PPreSynapticLayer);
        }

        public void PreTrainComplete()
        {
            pretraining = true;
            throw new NotImplementedException();
        }

        private void PerformPreTraining(int PPreSynapticLayer)
        {
            RestrictedBoltzmannMachineLearningRate sentlearnrate = new RestrictedBoltzmannMachineLearningRate(learnrate.preLearningRateWeights[PPreSynapticLayer],learnrate.preLearningRateBiases[PPreSynapticLayer],learnrate.preMomentumWeights[PPreSynapticLayer],learnrate.preMomentumBiases[PPreSynapticLayer]);

            RestrictedBoltzmannMachineTrainer.Train(layers[PPreSynapticLayer], layers[PPreSynapticLayer + 1],trainingData[PPreSynapticLayer],sentlearnrate,recognitionweights.GetWeightSet(PPreSynapticLayer));
        }


        public void Save(string PFilename)
        {
            TextWriter file = new StreamWriter(PFilename);
            learnrate.Save(file);
            recognitionweights.Save(file);
            generateiveweights.Save(file);
            file.WriteLine(numlayers);
            for (int i = 0; i < numlayers; i++)
            {
                if (layers[i].GetType() == typeof(RestrictedBoltzmannMachineGaussianLayer))
                {
                    file.WriteLine("RestrictedBoltzmannMachineGaussianLayer");
                }
                else if (layers[i].GetType() == typeof(RestrictedBoltzmannMachineBinaryLayer))
                {
                    file.WriteLine("RestrictedBoltzmannMachineBinaryLayer");
                }
                layers[i].Save(file);
            }
            file.WriteLine(pretraining);
            file.Close();
        }

        public static Autoencoder Load(string PFilename)
        {
            TextReader file = new StreamReader(PFilename);
            Autoencoder retval = new Autoencoder();
            retval.learnrate = new AutoencoderLearningRate();
            retval.learnrate.Load(file);
            retval.recognitionweights = new AutoencoderWeights();
            retval.recognitionweights.Load(file);
            retval.generateiveweights = new AutoencoderWeights();
            retval.generateiveweights.Load(file);
            retval.numlayers = int.Parse(file.ReadLine());
            retval.layers = new RestrictedBoltzmannMachineLayer[retval.numlayers];
            for (int i = 0; i < retval.numlayers; i++)
            {
                string type = file.ReadLine();
                if (type == "RestrictedBoltzmannMachineGaussianLayer")
                {
                    retval.layers[i] = new RestrictedBoltzmannMachineGaussianLayer();
                }
                else if (type == "RestrictedBoltzmannMachineBinaryLayer")
                {
                    retval.layers[i] = new RestrictedBoltzmannMachineBinaryLayer();
                }
                retval.layers[i].Load(file);
            }
            retval.pretraining = bool.Parse(file.ReadLine());
            retval.InitializeTrainingData();
            retval.errorobservers = new List<IErrorObserver>();
            file.Close();
            return retval;
        }
    }
}
