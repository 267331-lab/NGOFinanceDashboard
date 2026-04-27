using System;
using System.Collections.Generic;

using SafetyAlarm.Rules;
using  SafetyAlarm.MachineData;

namespace SafetyAlarm
{
    public class AlarmEngine
{
    private readonly List<AlarmRule> _rules;

    public event EventHandler<AlarmTriggeredEventArgs>? OnAlarmTriggered;

    public AlarmEngine(List<AlarmRule> rules)
    {
        _rules = rules ?? throw new ArgumentNullException(nameof(rules));

        foreach (var rule in _rules)
            rule.OnAlarmTriggered += (sender, e) => OnAlarmTriggered?.Invoke(sender, e);
    }

    public void Evaluate(IEnumerable<MachineRecord> records)
    {
        foreach (var record in records)
            foreach (var rule in _rules)
                rule.Evaluate(record); 
    }
}

    public class AlarmTriggeredEventArgs : EventArgs
    {
        public string? RuleName   { get; init; }
        public string? MachineId  { get; init; }
        public DateTime Timestamp { get; init; }
        public string? Message    { get; init; }
    }

}