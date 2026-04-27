namespace SafetyAlarm.MachineData.Event
{
    public class BadLineFoundEventArgs : EventArgs
    {
        public string? LineContent { get; set; }
        public int LineNumber { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class BadLineLogger
{
    public void Handle(object? sender, BadLineFoundEventArgs e)
    {
        Console.WriteLine($"[WARNING] Skipping line {e.LineNumber}: {e.ErrorMessage}");
        Console.WriteLine($"  Content: {e.LineContent}");
    }
}    
    
}