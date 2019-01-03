using System;
using System.IO;

namespace GeneticAlgorithm
{
    class Program
    {
        StreamWriter streamWriter;
        int maxNumberOfChromosomes = 30;
        int maxLenght = 64;
        int generations = 0;
        Chromosome[] chromosomes;
        int ct = 0;
        Random random = new Random();

        void initializeFile()
        {
            try
            {
                streamWriter = new StreamWriter("Out.txt");
            }
            catch (Exception ex)
            {
                ct++;
                initializeFile();
                Console.WriteLine(ex);
            }
        }

        void initializeAlgorithm()
        {
            Console.WriteLine("Number of steps:");
            generations = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Number of chromosomes:");
            maxNumberOfChromosomes = Convert.ToInt32(Console.ReadLine());

            chromosomes = new Chromosome[maxNumberOfChromosomes];

            for (int i = 0; i < maxNumberOfChromosomes; i++)
            {
                chromosomes[i] = new Chromosome();
                //generate random population
                chromosomes[i].x = random.NextDouble() * 5;
                chromosomes[i].valLong = doubleToLong(chromosomes[i].x);
            }
        }

        //convert x from double to long
        public long doubleToLong(double x)
        {
            long bits = (long)BitConverter.DoubleToInt64Bits(x);
            return bits;
        }

        static void Main(string[] args)
        {
        }
    }
}
