namespace PackagingCellCycle
{
    public class Operation
    {
        public string Name { get; }

        public Operation(string name)
        {
            Name = name;
        }

        public virtual void Execute()
        {
            
        }
    }
}