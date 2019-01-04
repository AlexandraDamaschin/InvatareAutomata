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

                //foreach chromosome from the entire population calculate trust 
                //using fitness function
                chromosomes[i].valFunction = fitness(chromosomes[i].x);
            }
        }

        void Algorithm()
        {
            //initialize algorithm
            initializeAlgorithm();

            ordonateChromosomes();
            writeChromosomes();

            for (int i = 0; i < generations; i++)
            {
                for (int j = maxNumberOfChromosomes / 2; j < maxNumberOfChromosomes; j++)
                {
                    int probability = random.Next(100);

                    if (probability < 20)
                    {
                        applyMutation(j);
                    }
                    else
                    {
                        if (j < maxNumberOfChromosomes - 1)
                        {
                            applyCrossover(j, 2);
                            j++;
                        }
                        else
                        {
                            applyCrossover(j, 1);
                        }
                    }
                }
                ordonateChromosomes();
                writeChromosomes();
                Console.WriteLine(i);
            }
            streamWriter.Close();
            test();
            Console.Read();
        }

        #region transforms
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

        #endregion

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
                selectedChromosome.valFunction = fitness(selectedChromosome.x);

                if (selectedChromosome.x >= 0 && selectedChromosome.x <= 5)
                {
                    chromosomes[chromosomeIndex] = selectedChromosome;
                }
                else
                {
                    ok = false;
                }

            } while (!ok);
        }

        //apply crossover
        void applyCrossover(int chromosomeIndex, int kidsNumber)
        {
            int indexParent1 = selectRandomChromosome();
            int indexParent2 = selectRandomChromosome();
            long temp;
            bool ok = false;

            //in case both parents are the same select again
            while (indexParent1 == indexParent2)
            {
                indexParent2 = selectRandomChromosome();
            }

            Chromosome parent1 = copyChromosome(chromosomes[indexParent1]);
            Chromosome parent2 = copyChromosome(chromosomes[indexParent2]);
            Chromosome kid = new Chromosome();

            do
            {
                ok = true;
                long selectionValue = 0;

                for (int i = 0; i < maxLenght - 1; i++)
                {
                    long bit = (long)random.Next(2);
                    selectionValue = (selectionValue << 1) + bit;
                }

                temp = (parent1.valLong & (~selectionValue));
                temp = temp & parent2.valLong;

                kid.valLong = temp;
                kid.x = longToDouble(temp);
                kid.valFunction = fitness(kid.x);

                if (kid.x >= 0 & kid.x <= 5)
                {
                    chromosomes[chromosomeIndex] = kid;
                }
                else
                {
                    ok = false;
                }

                if (kidsNumber == 2)
                {
                    temp = parent1.valLong & selectionValue;
                    temp = temp & (~parent2.valLong);

                    kid.valLong = temp;
                    kid.x = longToDouble(temp);
                    kid.valFunction = fitness(kid.x);

                    if (kid.x >= 0 & kid.x <= 5)
                    {
                        chromosomes[chromosomeIndex + 1] = kid;
                    }
                    else
                    {
                        ok = false;
                    }
                }
            } while (!ok);
        }

        //ordonate chromosomes
        void ordonateChromosomes()
        {
            Chromosome chromosom;
            int flag;

            do
            {
                flag = 0;

                for (int i = 0; i < maxNumberOfChromosomes - 1; i++)
                {
                    for (int j = i + 1; j < maxNumberOfChromosomes; j++)
                    {
                        if (chromosomes[i].valFunction > chromosomes[j].valFunction)
                        {
                            chromosom = chromosomes[i];
                            chromosomes[i] = chromosomes[j];
                            chromosomes[j] = chromosom;
                            flag = 1;
                        }
                    }
                }

            } while (flag == 1);
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

        void test()
        {
            for (double d = 0; d < 5; d += 0.2)
            {
                Console.WriteLine(d + ":" + fitness(d));
            }
        }

        static void Main(string[] args)
        {
            initializeAlgorithm();
            Algorithm();
        }
    }
}
