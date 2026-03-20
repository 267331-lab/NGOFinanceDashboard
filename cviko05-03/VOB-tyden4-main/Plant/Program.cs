using Plant.Services;
using Plant.Core;

namespace Plant;

class Program
{
    public delegate void StationActions();
    static void Main()
    {
        Console.WriteLine("=== Delegate + callback demo ===");
        var interlock = new SafetyInterlock();
        var ops = new StationOperations("Station-A");

        // TASK:
        // Execute station operations through the SafetyInterlock object.
        // Either define the delegate (as a local variable) or pass methods directly
        // in a method expectiong a delegate
        // Pass the StationOperation methods as callbacks so that they are executed
        // inside the guarded sequence managed by SafetyInterlock.
        //
        // Run at least two different operations in this way.

        Action stationActions = ops.PulseValve;
        
        SafetyInterlock.RunGuarded(stationActions);
        
        stationActions += ops.JogAxis;

        SafetyInterlock.RunGuarded(stationActions);

        Console.WriteLine("Done.");
        Console.ReadKey();
    }
}

