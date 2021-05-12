using System;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            NNManager mgr = new NNManager();
            mgr
            .SetupNetwork()
            .GetTrainingDataFromUser()
            .TrainNetworkToMininum()
            .TestNetwork();

            Console.ReadKey();
        }
    }
}
