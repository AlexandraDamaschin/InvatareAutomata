using System;
using System.IO;

namespace GeneticAlgorithm
{
    class Program
    {
        StreamWriter streamWriter;
        int maxNumber = 30;
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
        

        static void Main(string[] args)
        {
        }
    }
}
