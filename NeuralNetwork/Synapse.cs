using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    /// <summary>
    /// 突触
    /// </summary>
    public class Synapse
    {
        public Synapse() { }
        public Synapse(Neuron inputNeuron, Neuron outputNeuron)
        {
            Id = Guid.NewGuid();
            InputNeuron = inputNeuron;
            OutputNeuron = outputNeuron;
        }

        public Guid Id { get; set; }
        //输入神经元
        public Neuron InputNeuron { get; set; }
        //输出神经元
        public Neuron OutputNeuron { get; set; }
        //权重
        public double Weight { get; set; }
        //权重增量
        public double WeghtDelta { get; set; }
    }
}
