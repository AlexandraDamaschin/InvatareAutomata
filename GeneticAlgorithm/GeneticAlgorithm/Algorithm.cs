using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm
{
    class Algorithm
    {
        #region Properties
        private Random random;
        private List<Chromosome> Chromosomes = new List<Chromosome>();
        private Population population;
        private int numberOfChromosoms;
        #endregion

        #region Constructors
        public Algorithm(int numberOfChromosoms)
        {
            random = new Random();
            this.numberOfChromosoms = numberOfChromosoms;
        }
        #endregion

        #region methods
        public void Run()
        {
            // Generate random population
            GenerateData();
            population = new Population(Chromosomes, random, numberOfChromosoms);

            int i = 0;
            while (i < 100)
            {
                population.CodificationOfData();
                population.CalculateTotalFitness();
                population.PrintChromosomees();
                population.NextGeneration();
                population.PrintChromosomees();
                i++;
            }
        }

        private void GenerateData()
        {
            for (int i = 0; i < numberOfChromosoms; i++)
            {
                double value = random.NextDouble() * 5;
                Chromosomes.Add(new Chromosome(value));
            }
        }
        #endregion
    }
}
