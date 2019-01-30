using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackPropagationPoints
{
    class Layer
    {
        public List<Neuron> Neurons { get; set; }

        public Layer(int noNeurons)
        {
            Neurons = new List<Neuron>();

            for (int i = 0; i < noNeurons; i++)
            {
                Neurons.Add(new Neuron());
            }
        }

        public Layer(int noNeurons, int noWeights)
        {
            Neurons = new List<Neuron>();

            for (int i = 0; i < noNeurons; i++)
            {
                Neurons.Add(new Neuron(noWeights));
            }
        }
    }
}
