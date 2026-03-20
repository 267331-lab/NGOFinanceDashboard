public class Measurement<T>
{
    public T Value;
    public string Unit;

    public Measurement(T value, string unit)
    {
        Value = value;
        Unit = unit;

    }

    public void Describe()
    {
        Console.WriteLine(Value);
        Console.WriteLine(Unit);
        }
}

public interface IStartable
{
    void Start();
}
public class Machine:IStartable
{
    public void Start()
    {
        Console.WriteLine("Machine started");
    }
}



public class MachineOperator<T> where T : IStartable
{
    private T _machine;

    public MachineOperator(T machine)
    {
        _machine = machine;
    }

    public void Operate()
    {
        _machine.Start();
    }
}
class Program
{
    static void Main(string[] args)
    {
        Measurement<bool> measurement = new(true, "jednotka");
        measurement.Describe();

        Machine machine = new();
        var op = new MachineOperator<Machine>(machine);
        op.Operate(); // Machine started
    }
}

