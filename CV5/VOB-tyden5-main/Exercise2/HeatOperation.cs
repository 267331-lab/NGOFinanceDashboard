namespace PackagingCellCycle
{
    class HeatOperation : Operation
    {
        public double TemperatureC { get; }

        public HeatOperation(string name, double temperatureC) : base(name)
        {
            TemperatureC = temperatureC;
        }

        public override void Execute()
        {
             Console.WriteLine($"Heat operation {Name}: heated part to {TemperatureC} C.");
        }
    }
}