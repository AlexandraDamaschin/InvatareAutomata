namespace GeneticAlgorithm
{
    class Chromosome
    {
        private double gene;
        public double Genes { get => gene; set => gene = value; }

        public string BinaryGenes { get; set; }

        public double Score { get; set; }

        public Chromosome() { }

        public Chromosome(double gene)
        {
            this.gene = gene;
        }
    }
}
