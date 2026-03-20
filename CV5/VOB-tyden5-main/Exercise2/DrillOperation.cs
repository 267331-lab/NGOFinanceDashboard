namespace PackagingCellCycle
{
    class DrillOperation : Operation
    {
        public double DepthMm { get; }

        public DrillOperation(string name, double depthMm) : base(name)
        {
            DepthMm = depthMm;
        }

        public override void Execute()
        {
            Console.WriteLine($"Drill operation {Name}: drilled hole to depth {DepthMm} mm.");
        }
    }
}