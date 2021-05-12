using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class Network
    {
        //学习率
        public double LearningRate { get; set; }
        //动量
        public double Momentum { get; set; }
        //输入层
        public List<Neuron> InputLayer { get; set; }
        //隐藏层
        public List<List<Neuron>> HiddenLayers { get; set; }
        //输出层
        public List<Neuron> OutputLayer { get; set; }
        //镜像层
        public List<Neuron> MirrorLayer { get; set; }
        //典型层
        public List<Neuron> CanonicalLayer { get; set; }

        public Network()
        {
            InputLayer = new List<Neuron>();
            HiddenLayers = new List<List<Neuron>>();
            OutputLayer = new List<Neuron>();
            CanonicalLayer = new List<Neuron>();
        }

        public Network(int numInputParameters, int[] hiddenNeurons, int numOutputParameters) : this()
        {
            //构建输入层_numInputParameters = numInputParameters;
            for (int i = 0; i < numInputParameters; i++)
            {
                Neuron inputNeuron = new Neuron
                {
                    Id = Guid.NewGuid()
                };
                InputLayer?.Add(inputNeuron);
            }

            //构建隐藏层_hiddenNeurons = hiddenNeurons;
            for (int i = 0; i < hiddenNeurons.Length; i++)
            {
                List<Neuron> hiddenLayer = new List<Neuron>();
                for (int j = 0; j < hiddenNeurons[i]; j++)
                {
                    Neuron hiddenNeuron = new Neuron
                    {
                        Id = Guid.NewGuid()
                    };
                    hiddenLayer?.Add(hiddenNeuron);
                }
                HiddenLayers?.Add(hiddenLayer);
            }

            //构建输出层_numOutputParameters = numOutputParameters;
            for (int i = 0; i < numOutputParameters; i++)
            {
                Neuron outputNeuron = new Neuron
                {
                    Id = Guid.NewGuid()
                };
                OutputLayer?.Add(outputNeuron);
            }

            //突触怎么连接呢？
            for (int i = 0; i < HiddenLayers.Count; i++)
            {
                foreach (var hiddenNeuron in HiddenLayers[i])
                {
                    //如果是隐藏层的一次层，输入层和隐藏层[0]
                    if (i == 0)
                    {
                        foreach (var inputNeuron in InputLayer)
                        {
                            Synapse synapse = new Synapse(inputNeuron, hiddenNeuron);
                            inputNeuron.OutputSynapses?.Add(synapse);
                            hiddenNeuron.InputSynapses?.Add(synapse);
                        }
                    }
                    //如果是除了第一层的其它层，隐藏层[i-1]和隐藏层[i]
                    if (i > 0)
                    {
                        foreach (var forwarkHiddenNeuron in HiddenLayers[i - 1])
                        {
                            Synapse synapse = new Synapse(forwarkHiddenNeuron, hiddenNeuron);
                            forwarkHiddenNeuron.OutputSynapses?.Add(synapse);
                            hiddenNeuron.InputSynapses?.Add(synapse);
                        }
                    }
                    //如果是最后一层，隐藏层[count-1]和输出层
                    if (i == HiddenLayers.Count - 1)
                    {
                        foreach (var outNeuron in OutputLayer)
                        {
                            Synapse synapse = new Synapse(hiddenNeuron, outNeuron);
                            hiddenNeuron.OutputSynapses?.Add(synapse);
                            outNeuron.InputSynapses?.Add(synapse);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 前向传播
        /// </summary>
        /// <param name="inputs"></param>
        private void ForwardPropagate(params double[] inputs)
        {
            var i = 0;
            //输入层 输入值
            InputLayer?.ForEach(a => a.Value = inputs[i++]);
            //隐藏层 计算值
            HiddenLayers?.ForEach(a => a.ForEach(b => b.CalculateValue()));
            //输出层 计算值
            OutputLayer?.ForEach(a => a.CalculateValue());
        }

        /// <summary>
        /// 后向传播
        /// </summary>
        /// <param name="targets"></param>
        private void BackPropagate(params double[] targets)
        {
            var i = 0;
            //输出层 计算梯度
            OutputLayer?.ForEach(a => a.CalculateGradient(targets[i++]));

            //隐藏层操作
            HiddenLayers?.Reverse();
            //隐藏层 计算梯度
            HiddenLayers?.ForEach(a => a.ForEach(b => b.CalculateGradient()));
            //隐藏层 更新权重
            HiddenLayers?.ForEach(a => a.ForEach(b => b.UpdateWeights(LearningRate, Momentum)));
            HiddenLayers?.Reverse();

            //输出层 更新权重
            OutputLayer?.ForEach(a => a.UpdateWeights(LearningRate, Momentum));
        }

        /// <summary>
        /// 网络运算
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public double[] Compute(params double[] inputs)
        {
            ForwardPropagate(inputs);
            return OutputLayer.Select(a => a.Value).ToArray();
        }

        public void Train(List<DataSet> dataSets, int numEpochs)
        {
            for (var i = 0; i < numEpochs; i++)
            {
                foreach (var dataSet in dataSets)
                {
                    ForwardPropagate(dataSet.Values);
                    BackPropagate(dataSet.Targets);
                }
            }
        }
    }
}
