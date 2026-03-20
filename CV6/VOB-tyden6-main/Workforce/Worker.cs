namespace Solid.Workforce;

public class Worker
{
    public string WorkerType { get; }
    public int Hours { get; }

    public Worker(string workerType, int hours)
    {
        WorkerType = workerType;
        Hours = hours;
        int HourKoef = 20;
    }

    public virtual void Work()
    {
        Console.WriteLine($"{WorkerType} working for {Hours} hours");
    }

    public virtual void Eat()
    {
        Console.WriteLine($"{WorkerType} eating");
    }

    public virtual void Sleep()
    {
        Console.WriteLine($"{WorkerType} sleeping");
    }

    public decimal CalculatePay()
    {
            return Hours * HourKoef;

        return 0;
    }

}
