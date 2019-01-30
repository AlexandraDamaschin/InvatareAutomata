using System;
using System.Collections.Generic;
using System.Linq;

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

            Layers.Add(new Layer(noInputs, 0));
            Layers.Add(new Layer(noHiddens, noInputs));
            Layers.Add(new Layer(noOutputs, noHiddens));
        }

        public void Forward(double[] point)
        {
            for (int i = 0; i < Layers[0].Neurons.Count; i++)
            {
                Layers[0].Neurons[i].Output = point[i];
            }

            for (int i = 1; i < Layers.Count(); i++)
            {
                for (int j = 0; j < Layers[i].Neurons.Count; j++)
                {
                    double sum = Layers[i].Neurons[j].W0;

                    for (int k = 0; k < Layers[i].Neurons[j].Weight.Length; k++)
                    {
                        sum += Layers[i].Neurons[j].Weight[k] * Layers[i - 1].Neurons[k].Output;
                    }

                    Layers[i].Neurons[j].ActivationFunction(sum);
                }
            }
        }

        public double TotalError(double target)
        {
            double total = 0.0d;

            for (int i = 0; i < Layers[Layers.Count - 1].Neurons.Count; i++)
            {
                total += Math.Pow((Layers[Layers.Count - 1].Neurons[i].Output - target), 2);
            }
            return total;
        }

        public void Backward1(double target)
        {
            double error;
            for (int i = 0; i < Layers[Layers.Count - 1].Neurons.Count; i++)
            {
                error = Layers[Layers.Count - 1].Neurons[i].Output - target;
                Layers[Layers.Count - 1].Neurons[i].Error = 2 * error * Layers[Layers.Count - 1].Neurons[i].Derivata();
            }
        }

        public void Backward(double target)
        {
            for (int i = 0; i < Layers[Layers.Count - 1].Neurons.Count; i++)
            {
                Layers[Layers.Count - 1].Neurons[i].Error = 2 * (Layers[Layers.Count - 1].Neurons[i].Output - target) *
                                                            Layers[Layers.Count - 1].Neurons[i].Derivata();

                Layers[Layers.Count - 1].Neurons[i].W0 += -learningRate * Layers[Layers.Count - 1].Neurons[i].Error;

                for (int j = 0; j < Layers[Layers.Count - 1].Neurons[i].Weight.Length; j++)
                {
                    Layers[Layers.Count - 1].Neurons[i].Weight[j] += -learningRate * Layers[Layers.Count - 1].Neurons[i].Error *
                                                                      Layers[Layers.Count - 2].Neurons[j].Output;
                }
            }

            for (int i = 0; i < Layers[Layers.Count - 2].Neurons.Count; i++)
            {
                double sum = 0.0d;
                for (int j = 0; j < Layers[Layers.Count - 1].Neurons.Count; j++)
                {
                    sum += (Layers[Layers.Count - 1].Neurons[j].Output - target) * Layers[Layers.Count - 1].Neurons[j].Derivata();
                }
                Layers[Layers.Count - 2].Neurons[i].Error = 2 * sum * Layers[Layers.Count - 2].Neurons[i].Derivata();

                Layers[Layers.Count - 2].Neurons[i].W0 += -learningRate * Layers[Layers.Count - 2].Neurons[i].Error;

                sum = 0.0d;

                for (int j = 0; j < Layers[0].Neurons.Count; j++)
                {
                    Layers[Layers.Count - 2].Neurons[i].Weight[j] += -learningRate * Layers[Layers.Count - 2].Neurons[i].Error * Layers[0].Neurons[j].Output;
                }
            }
        }
    }
}
