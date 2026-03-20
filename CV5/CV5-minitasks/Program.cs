namespace Devices{
public class Device{
protected string Name;

public Device(string name)
    {
        Name = name;
    }

public virtual void PrintInfo()
    {
        Console.WriteLine(Name);
    }
}

public class Sensor : Device
{
    public Sensor() : base("Sensor") { }
}

public class TemperatureSensor : Sensor
{
    public double Value;
    public TemperatureSensor(string value)
    {
        Value = double.Parse(value);
    }
    public override void PrintInfo()
    {
        Console.WriteLine(Name);
    }
}

public class PressureSensor : Sensor
{
    public double PressureBar;
    public PressureSensor(string pressureBar)
    {
        PressureBar = double.Parse(pressureBar);
    }
    public override void PrintInfo()
    {
        Console.WriteLine(Name);
    }
}


public class Program
{
    public static void Main()
    {
        Device device = new("Device");
        Sensor sensor = new();
        TemperatureSensor temperatureSensor = new("25.5");
        PressureSensor pressureSensor = new("1.013");

        Device deviceCasting = (Device)temperatureSensor;
        TemperatureSensor TempFromDevice = (TemperatureSensor)deviceCasting;
        if (TempFromDevice is Sensor)
            {
                Console.WriteLine("is Sensor");
            }
            else
            {
                Console.WriteLine("Not SENSOR");
            }

         List<Device> Devices = [device, sensor, temperatureSensor,pressureSensor ];
        for (int i = 1; i < Devices.Count; i++)
            {
                Devices[i].PrintInfo();
            }

    }
}
}