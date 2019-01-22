using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    class Population
    {
        private Random random;
        private List<Chromosome> Chromosomes = new List<Chromosome>();
        private double totalFitness;
        private int numberOfChromosomes; // Number of cromozons

        public Population(List<Chromosome> Chromosomes, Random random, int count)
        {
            this.Chromosomes = Chromosomes;
            this.random = random;
            this.numberOfChromosomes = count;
        }

        #region methods
        public void NextGeneration()
        {
            // Select "elite"
            var temp = Chromosomes.OrderBy(o => o.Score).ToList();

            Chromosomes.Clear();

            //take first half of the population with best score = 'elite'
            for (int i = 0; i < numberOfChromosomes / 2; i++)
            {
                Chromosomes.Add(temp[i]);
            }

            // Crossover for the other half od the generation
            while (Chromosomes.Count <= numberOfChromosomes)
            {
                // Select 2 Chromosoms
                Chromosome parent1 = Selection(temp);
                Chromosome parent2 = Selection(temp);

                // Crossover
                List<Chromosome> crs = Crossover(parent1, parent2);
                Chromosomes.Add(crs[0]);
                Chromosomes.Add(crs[1]);
            }

            // Mutation for 10%
            Mutation();
        }

        private void Mutation()
        {
            int count = numberOfChromosomes / 10;

             while (count > 0)
            {
                int i = random.Next(0, 100);

                bool bit;

                //convert gene of chromosome to binary
                BitArray binaryGene = new BitArray(BitConverter.GetBytes(Chromosomes[i].Genes));

                for (int j = random.Next(0, 50); j > 0; j--)
                {
                    bit = binaryGene.Get(j);
                    bit = !bit;
                    binaryGene.Set(j, bit);
                }
                byte[] tempbytes = new byte[binaryGene.Length];

                binaryGene.CopyTo(tempbytes, 0);
                //convert bynary gene to double
                Chromosomes[i].Genes = BitConverter.ToDouble(tempbytes, 0);

                count--;
            }
        }

        private Chromosome Selection(List<Chromosome> temp)
        {
            Chromosome bestChromosome = new Chromosome();
            double totalRandom = random.NextDouble() * totalFitness;
            double currentFitness = 0;

            //chose best chromosome from first half of the population
            for (int i = 0; i < numberOfChromosomes / 2; i++)
            {
                currentFitness += temp[i].Score;
                if (totalRandom > currentFitness)
                {
                    bestChromosome = temp[i];
                }
            }
            return bestChromosome;
        }

        private List<Chromosome> Crossover(Chromosome parent1, Chromosome parent2)
        {
            //convert gene from double to bit
            BitArray binaryGene1 = new BitArray(BitConverter.GetBytes(parent1.Genes));
            BitArray binaryGene2 = new BitArray(BitConverter.GetBytes(parent2.Genes));

            //BitArray BAB1, BAB2;
            bool temp1, temp2;
            for (int i = random.Next(0, 32); i > 0; i--)
            {
                temp1 = binaryGene1.Get(i);
                temp2 = binaryGene2.Get(i);

                binaryGene1.Set(i, temp2);
                binaryGene2.Set(i, temp1);
            }
            byte[] tempbytes = new byte[binaryGene1.Length];

            //copy to new generation
            binaryGene1.CopyTo(tempbytes, 0);
            parent1.Genes = BitConverter.ToDouble(tempbytes, 0);

            binaryGene2.CopyTo(tempbytes, 0);
            parent2.Genes = BitConverter.ToDouble(tempbytes, 0);
            
            //add them to the chromosome list
            List<Chromosome> crs = new List<Chromosome>();
            crs.Add(parent1);
            crs.Add(parent2);
            return crs;
        }

        public void CodificationOfData()
        {
            foreach (Chromosome Chromosome in Chromosomes)
            {
                //convert gene from double to long 
                long intValue = BitConverter.DoubleToInt64Bits(Chromosome.Genes);
                //convert gene from long to binary
                Chromosome.BinaryGenes = Convert.ToString(intValue, 2);
            }
        }

        public void CalculateTotalFitness()
        {
            totalFitness = 0;
            foreach (Chromosome Chromosome in Chromosomes)
            {
                Chromosome.Score = GetFitness(Chromosome.Genes);
                totalFitness += Chromosome.Score;
            }
        }

        double GetFitness(double x)
        {
            double value = Math.Sinh(Math.Cos(x) * Math.Cos(x) + 1);
            return value;
        }

        public double TestFitness(double x)
        {
            double value = GetFitness(x);
            return value;
        }

        public void PrintChromosomees()
        {
            Console.WriteLine("Chromosoms Start");
            foreach (Chromosome Chromosome in Chromosomes)
            {
                Console.WriteLine("Gene: " + Chromosome.Genes + " | FitnessScore: " + Chromosome.Score);
                //Console.WriteLine("Gene: " + Chromosome.BinaryGenes + " | Score: " + Chromosome.Score);
            }
            Console.WriteLine("End Chromosoms");
        }
        #endregion

    }
}
