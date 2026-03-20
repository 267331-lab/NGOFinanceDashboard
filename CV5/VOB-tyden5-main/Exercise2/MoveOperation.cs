namespace PackagingCellCycle
{
    class MoveOperation : Operation
    {
        public double DistanceMm { get; }

        public MoveOperation(string name, double distanceMm) : base(name)
        {
            DistanceMm = distanceMm;
        }

        public override void Execute()
        {
            Console.WriteLine($"Move operation {Name}: moved part by {DistanceMm} mm.DISTANCE");
        }
    }
}