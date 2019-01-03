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

                //fitness function?
            }
        }

        //convert x from double to long
        public long doubleToLong(double x)
        {
            long bits = (long)BitConverter.DoubleToInt64Bits(x);
            return bits;
        }

        //convert x from long to double
        public double longToDouble(long x)
        {
            return BitConverter.Int64BitsToDouble(x);
        }

        //write chromosomes 
        void writeChromosomes()
        {
            for (int i = 0; i < maxNumberOfChromosomes; i++)
            {
                streamWriter.Write(chromosomes[i].valFunction + ";" + chromosomes[i].x + ";" + ";");
                streamWriter.WriteLine();
            }
        }

        //apply mutation
        void applyMutation(int chromosomeIndex)
        {
            int selectedChromosom = selectRandomChromosome();
            Chromosome selectedChromosome = copyChromosome(chromosomes[chromosomeIndex]);

            int numberOfChangedValues = random.Next(1, maxLenght);
            int indexByte;
            bool ok;

            do
            {
                ok = true;
                for (int i = 0; i < numberOfChangedValues; i++)
                {
                    indexByte = random.Next(maxLenght);
                    selectedChromosome.valLong = mutationChromosome(selectedChromosome.valLong, indexByte);
                }

                selectedChromosome.x = longToDouble(selectedChromosome.valLong);
            } while (!ok);

        }

        //select random chromosom
        int selectRandomChromosome()
        {
            double total = 0, sum = 0;
            double rand = random.NextDouble();

            for (int i = 0; i < maxNumberOfChromosomes / 2; i++)
            {
                total += chromosomes[i].valFunction;
            }

            for (int i = 0; i < maxNumberOfChromosomes / 2; i++)
            {
                if ((rand > sum) && (rand < (sum + 1 / chromosomes[i].valFunction) / total))
                {
                    return i;
                }
                sum += (1 / chromosomes[i].valFunction) / total;
            }

            return maxNumberOfChromosomes / 2;
        }

        //copy chromosome
        Chromosome copyChromosome(Chromosome chromosome)
        {
            Chromosome newChromosome = new Chromosome();
            newChromosome.x = chromosome.x;
            newChromosome.valLong = chromosome.valLong;
            newChromosome.valFunction = chromosome.valFunction;

            return newChromosome;
        }

        //make mutation of a chromosome
        long mutationChromosome(long binarChromosome, int indexMutation)
        {

            binarChromosome ^= (long)(1 << indexMutation);
            return binarChromosome;
        }

        //fitness function = we want to find out the min
        public double fitness(double val)
        {
            return Math.Sinh(Math.Cos(val) * Math.Cos(val) + 1);
        }


        static void Main(string[] args)
        {
        }
    }
}
