namespace Solid;

using Solid.Messsages;
using Solid.FileSystem;
using Solid.Workforce;

internal class App
{
    public void Run()
    {
        var worker = new RobotWorker("robot", 8);

        worker.Work();
        Console.WriteLine($"Pay: {worker.CalculatePay()}");
        FileManager.SaveToFile("worker.txt",worker);
        EmailSender.SendEmail("boss@company.com");
        worker.Eat();
    }
}

// worker.SaveToFile 
// Werker calculate pay, not extecible
//