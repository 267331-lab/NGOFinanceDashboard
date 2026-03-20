
namespace Solid.Messsages{
public static class EmailSender
{
    public static void SendEmail(string email)
    {
        Console.WriteLine($"Sending worker report to {email}");
    } 
}
}
namespace Solid.FileSystem{
    using Solid.Workforce;
public static class FileManager
{
        public static void SaveToFile(string path, Worker worker )
    {
        File.WriteAllText(path, $"type={worker.WorkerType},hours={worker.Hours}{Environment.NewLine}");
    }
}
}

namespace Solid.WorkManager{
    public class HRDepartment{
        public decimal CalculatePay(IsWorking worker )
            {
                return worker.Hours * worker.HourKoef;

            }
    }
}