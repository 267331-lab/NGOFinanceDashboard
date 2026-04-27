public class Program
{
    public static void Main()
    {
        Task2 task = new Task2();
        task.Run();
    }
}

public class Task2
{
    public void Run()
    {
        var parts = new List<MachinePart>
        {
            new MachinePart { Id = 1, Name = "Shaft", Weight = 12.5, IsInStock = true },
            new MachinePart { Id = 2, Name = "Bolt", Weight = 0.3, IsInStock = true },
            new MachinePart { Id = 3, Name = "Housing", Weight = 18.2, IsInStock = false },
            new MachinePart { Id = 4, Name = "Gear", Weight = 6.8, IsInStock = true },
            new MachinePart { Id = 5, Name = "Bearing", Weight = 1.2, IsInStock = true }
        };

        PrintHeader();

        var heavyParts = parts.Where(p => p.IsInStock).Where(p => p.Weight > 5.0).ToList();
        var reportLines = heavyParts.Select(p => $"{p.Name} - {p.Weight} kg").ToList();
        

        PrintReport(reportLines);
    }

    void PrintHeader()
    {
        Console.WriteLine("Machine parts report");
        Console.WriteLine("--------------------");
    }

    void PrintReport(List<string> lines)
    {
        lines.ForEach(l => Console.WriteLine(l));
    }
}

public class MachinePart
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public double Weight { get; set; }
    public bool IsInStock { get; set; }
}