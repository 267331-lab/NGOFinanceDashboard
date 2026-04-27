namespace SafetyAlarm.Rules;

using System;
using SafetyAlarm.MachineData;

public abstract class AlarmRule
{
     public abstract string RuleName { get; }

    public event EventHandler<AlarmTriggeredEventArgs>? OnAlarmTriggered;

    public abstract void Evaluate(MachineRecord record); // ← už nevrací AlarmResult

    protected void RaiseAlarm(MachineRecord record, string message) =>
        OnAlarmTriggered?.Invoke(this, new AlarmTriggeredEventArgs
        {
            RuleName  = RuleName,
            MachineId = record.MachineId,
            Timestamp = record.Timestamp,
            Message   = message
        });
}

public class AlarmResult
{
    public bool IsTriggered { get; init; }
    public string? RuleName { get; init; }
    public string? MachineId { get; init; }
    public DateTime Timestamp { get; init; }
    public string? Message { get; init; }

    public override string ToString() =>
        IsTriggered
            ? $"[ALARM] {Timestamp:yyyy-MM-dd HH:mm:ss} | {MachineId} | {RuleName}: {Message}"
            : $"[OK]    {Timestamp:yyyy-MM-dd HH:mm:ss} | {MachineId} | {RuleName}";
}

public class HighTemperatureRule : AlarmRule
{
    public override string RuleName => "High Temperature";

    public double ThresholdCelsius { get; }

    public HighTemperatureRule(double thresholdCelsius = 80.0)
    {
        ThresholdCelsius = thresholdCelsius;
    }

    public override void Evaluate(MachineRecord record)
    {
        if (record.Temperature > ThresholdCelsius)
            RaiseAlarm(record, $"Temperature {record.Temperature}°C exceeds {ThresholdCelsius}°C");
    }
}

public class HighVibrationRule : AlarmRule
{
    public override string RuleName => "High Vibration";

    public double ThresholdMms { get; }

    public HighVibrationRule(double thresholdMms = 5.0)
    {
        ThresholdMms = thresholdMms;
    }

    public override void Evaluate(MachineRecord record)
    {
        if (record.Vibration > ThresholdMms)
            RaiseAlarm(record, $"Vibration level{record.Vibration} exceeds treshold {ThresholdMms}");
    }
}

public class HighCurrentRule : AlarmRule
{
    public override string RuleName => "High Current";

    public double ThresholdAmperes { get; }

    public HighCurrentRule(double thresholdAmperes = 10.0)
    {
        ThresholdAmperes = thresholdAmperes;
    }

    public override void Evaluate(MachineRecord record)
    {
        if (record.Current > ThresholdAmperes)
            RaiseAlarm(record, $"Vibration level{record.Current} exceeds treshold {ThresholdAmperes}");
    }
}



