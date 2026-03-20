namespace PackagingCellCycle
{
    class CycleRunner
    {
        public void Run(List<Operation> operations)
        {
            foreach (Operation operation in operations)
            {
                operation.Execute();
            }
            
        }
    }
}