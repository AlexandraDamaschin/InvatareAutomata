using System;

namespace BackPropagationPoints
{
    class Neuron
    {
        public double[] Weight { get; set; }
        public double W0 { get; set; } // prag
        public double Output { get; set; }
        public double Error { get; set; }

        public static Random rand = new Random();

        public Neuron()
        {
        }

        public Neuron(int noWeights)
        {
            Weight = new double[noWeights];
            Init();
        }

        public void Init()
        {
            for (int i = 0; i < Weight.Length; i++)
            {
                Weight[i] = rand.Next(6000) / 6000.0;
            }

            W0 = rand.Next(6000) / 6000.0;
        }

        public void ActivationFunction(double sumWeights)
        {
            Output = 1.0 / (1 + Math.Exp(-sumWeights));
        }

        public double Derivata()
        {
            return Output * (1 - Output);
        }
    }
}
