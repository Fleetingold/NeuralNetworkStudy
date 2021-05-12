using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetwork
{
    /// <summary>
    /// 神经元
    /// </summary>
    public class Neuron
    {
        public Guid Id { get; set; }
        //输入突触
        public List<Synapse> InputSynapses { get; set; }
        //输出突触
        public List<Synapse> OutputSynapses { get; set; }
        //偏差
        public double Bias { get; set; }
        //偏差增量
        public double BiasDelta { get; set; }
        //梯度
        public double Gradient { get; set; }
        //神经元的实际值
        public double Value { get; set; }
        //Mirror Neuron 镜像神经元
        public bool IsMirror { get; set; }
        //Canonical Neuron 典型神经元
        public bool IsCanonical { get; set; }

        public Neuron()
        {
            Id = Guid.NewGuid();
            InputSynapses = new List<Synapse>();
            OutputSynapses = new List<Synapse>();
        }

        public Neuron(IEnumerable<Neuron> inputNeurons) : this()
        {
            Ensure.That(inputNeurons).IsNotNull();

            foreach (var inputNeuron in inputNeurons)
            {
                var synapse = new Synapse(inputNeuron, this);
                inputNeuron?.OutputSynapses?.Add(synapse);
                InputSynapses?.Add(synapse);
            }
        }

        /// <summary>
        /// 计算误差
        /// </summary>
        public double CalculateError(double target)
        {
            return target - Value;
        }

        /// <summary>
        /// 计算梯度: 通过Sigmoid函数的导数来计算梯度
        /// </summary>
        /// <param name="v"></param>
        public double CalculateGradient(double? target = null)
        {
            if (target == null)
            {
                return Gradient = OutputSynapses.Sum(a => a.OutputNeuron.Gradient * a.Weight) * Sigmoid.Derivative(Value);
            }

            return Gradient = CalculateError(target.Value) * Sigmoid.Derivative(Value);
        }

        /// <summary>
        /// 更新权重
        /// </summary>
        /// <param name="learningRate"></param>
        /// <param name="momentum"></param>
        public void UpdateWeights(double learningRate, double momentum)
        {
            var prevDelta = BiasDelta;
            BiasDelta = learningRate * Gradient;
            Bias += BiasDelta + momentum * prevDelta;

            foreach (var synapse in InputSynapses)
            {
                prevDelta = synapse.WeghtDelta;
                synapse.WeghtDelta = learningRate * Gradient * synapse.InputNeuron.Value;
                synapse.Weight += synapse.WeghtDelta + momentum * prevDelta;
            }
        }

        /// <summary>
        /// 计算值
        /// </summary>
        public virtual double CalculateValue()
        {
            return Value = Sigmoid.Output(InputSynapses.Sum(a => a.Weight * a.InputNeuron.Value) + Bias);
        }
    }
}
