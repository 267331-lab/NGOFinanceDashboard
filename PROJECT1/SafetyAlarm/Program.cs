using System;
using SafetyAlarm;
using SafetyAlarm.Rules;
using SafetyAlarm.MachineData;

namespace SafetyAlarm
{
    internal class Program
    {
        static void Main()
        {
            var loader = new CsvLoader("telemetry.csv");

            var logger = new MachineData.Event.BadLineLogger();

            loader.OnBadLineFound += logger.Handle;
            var records = loader.LoadRawDataToRecords();
            Console.WriteLine($"Loaded {records.Count} record(s).\n");

            
            List<AlarmRule> rules = new()
                {
                    new HighTemperatureRule(thresholdCelsius: 60.0),
                    new HighVibrationRule(thresholdMms: 3.0),
                    new HighCurrentRule(thresholdAmperes: 8.0),
                };

            var engine = new AlarmEngine(rules);

            engine.OnAlarmTriggered += (sender, e) =>
                Console.WriteLine($"[ALARM] {e.Timestamp:yyyy-MM-dd HH:mm:ss} | {e.MachineId} | {e.RuleName}: {e.Message}");

            engine.Evaluate(records);  
        }
    }
}