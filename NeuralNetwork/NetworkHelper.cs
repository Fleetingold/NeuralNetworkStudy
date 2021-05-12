using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetwork
{
    public class NetworkHelper
    {
        public static Network ImportNetwork()
        {
            //获取以前保存的网络的文件名。打开后，将其反序列化为我们将要处理的网络结构(如果由于某种原因无效，请中止该操作):
            var dn = GetHelperNetwork();
            if (dn == null)
                return null;

            //创建一个新网络和要填充的神经元列表:
            var network = new Network();
            var allNeurons = new List<Neuron>();

            //复制之前保存的学习率和动量:
            network.LearningRate = dn.LearningRate;
            network.Momentum = dn.Momentnum;

            //从导入的网络数据创建输入层:
            foreach (var n in dn.InputLayer)
            {
                var neuron = new Neuron
                {
                    Id = n.Id,
                    Bias = n.Bias,
                    BiasDelta = n.BiasDelta,
                    Gradient = n.Gradient,
                    Value = n.Value
                };

                network.InputLayer?.Add(neuron);
                allNeurons.Add(neuron);
            }

            //从导入的网络数据创建隐藏层:
            foreach (var layer in dn.HiddenLayers)
            {
                var neurons = new List<Neuron>();
                foreach (var n in layer)
                {
                    var neuron = new Neuron
                    {
                        Id = n.Id,
                        Bias = n.Bias,
                        BiasDelta = n.BiasDelta,
                        Gradient = n.Gradient,
                        Value = n.Value
                    };

                    neurons.Add(neuron);
                    allNeurons.Add(neuron);
                }
                network.HiddenLayers?.Add(neurons);
            }

            //从导入的数据创建输出层神经元:
            foreach (var n in dn.OutputLayer)
            {
                var neuron = new Neuron
                {
                    Id = n.Id,
                    Bias = n.Bias,
                    BiasDelta = n.BiasDelta,
                    Gradient = n.Gradient,
                    Value = n.Value
                };

                network.OutputLayer?.Add(neuron);
                allNeurons.Add(neuron);
            }

            //最后，创建将所有内容联系在一起的突触:
            foreach (var syn in dn.Synapses)
            {
                var synapse = new Synapse { Id = syn.Id };
                var inputNeuron = allNeurons.First(x => x.Id == syn.InputNeuronId);
                var outputNeuron = allNeurons.First(x => x.Id == syn.OutputNeuronId);
                synapse.InputNeuron = inputNeuron;
                synapse.OutputNeuron = outputNeuron;
                synapse.Weight = syn.Weight;
                synapse.WeghtDelta = syn.WeghtDelta;

                inputNeuron?.OutputSynapses?.Add(synapse);
                outputNeuron?.InputSynapses.Add(synapse);
            }
            return network;
        }

        private static NetworkData GetHelperNetwork()
        {
            throw new NotImplementedException();
        }
    }

    public class NetworkData
    {
        public double LearningRate { get; set; }
        public double Momentnum { get; set; }
        public IEnumerable<Neuron> InputLayer { get; set; }
        public IEnumerable<IEnumerable<Neuron>> HiddenLayers { get; set; }
        public IEnumerable<Neuron> OutputLayer { get; set; }
        public IEnumerable<SynapseData> Synapses { get; set; }
    }

    public class SynapseData
    {
        public Guid Id { get; internal set; }
        public Guid InputNeuronId { get; internal set; }
        public Guid OutputNeuronId { get; internal set; }
        public double Weight { get; internal set; }
        public double WeghtDelta { get; internal set; }
    }
}
