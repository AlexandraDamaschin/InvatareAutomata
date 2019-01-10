using System.Collections.Generic;

namespace BackPropagationPoints
{
    class NeuralNetwork
    {
        private double learningRate;

        public List<Layer> Layers { get; set; }

        public NeuralNetwork(int noInputs, int noHiddens, int noOutputs, double learningRate = 1.0)
        {
            this.learningRate = learningRate;

            Layers = new List<Layer>();
        }
    }
}
