using System.Collections;
using System.Globalization;

using SafetyAlarm.MachineData.Event;

namespace SafetyAlarm.MachineData
{
    public class MachineRecord
    {
        public DateTime Timestamp { get; init; }
        public string? MachineId { get; init; }
        public double Temperature { get; init; }
        public double Vibration { get; init; }
        public double Current { get; init; }

        public override string ToString() =>
            $"MachineRecord(Timestamp={Timestamp:yyyy-MM-dd HH:mm:ss}, MachineId={MachineId}, " +
            $"Temperature={Temperature}, Vibration={Vibration}, Current={Current})";
    }

    public class CsvLoader : IEnumerable<MachineRecord>
    {
        private const char Delimiter = ';';
        private const string TimestampFormat = "yyyy-MM-dd HH:mm:ss";
        private static readonly string[] ExpectedColumns = { "timestamp", "machineId", "temperature", "vibration", "current" };
        
        private readonly List<MachineRecord> _records = new();
        public IReadOnlyList<MachineRecord> Records => _records.AsReadOnly();
        public event EventHandler<BadLineFoundEventArgs>? OnBadLineFound;
        public string FilePath { get; }
        public int Count => _records.Count;

        public CsvLoader(string filePath)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            CheckFileExists(FilePath);
        } 
        public IReadOnlyList<MachineRecord> LoadRawDataToRecords()
        { 
            using var reader = new StreamReader(FilePath);
            // --- Header ---
            Dictionary<string,int> index = LoadHeader(reader);
            // --- Data rows ---
            _records.Clear();
            int lineNumber = 1;
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var record = ParseRow(ParseLine(line), index);
                    _records.Add(record);
                }
                catch (Exception ex)
                {
                    RaiseBadLineFound(line, lineNumber, ex.Message);
                }
            }

            return Records;
        }
        private static void CheckFileExists(string FilePath)
        {
             if (!File.Exists(FilePath))
                throw new FileNotFoundException($"File not found: {FilePath}", FilePath);
        }
        private static Dictionary<string,int> LoadHeader(StreamReader reader){
            var headerLine = reader.ReadLine()
                ?? throw new InvalidDataException("CSV file is empty or has no header row.");
            var headers = ParseLine(headerLine);
            ValidateColumns(headers);
            var index = BuildColumnIndex(headers);
            return index;
        }     
        private static string[] ParseLine(string line) =>
            line.Split(Delimiter);
        private void RaiseBadLineFound(string lineContent, int lineNumber, string errorMessage) =>
            OnBadLineFound?.Invoke(this, new BadLineFoundEventArgs
            {
                LineContent  = lineContent,
                LineNumber   = lineNumber,
                ErrorMessage = errorMessage
            });
        private static void ValidateColumns(string[] headers)
        {
            var actual  = new HashSet<string>(headers, StringComparer.Ordinal);
            var missing = new List<string>();

            foreach (var col in ExpectedColumns)
                if (!actual.Contains(col))
                    missing.Add(col);

            if (missing.Count > 0)
                throw new InvalidDataException(
                    $"CSV is missing required columns: [{string.Join(", ", missing)}]. " +
                    $"Found columns: [{string.Join(", ", headers)}]");
        }
        private static Dictionary<string, int> BuildColumnIndex(string[] headers)
        {
            var index = new Dictionary<string, int>(StringComparer.Ordinal);
            for (int i = 0; i < headers.Length; i++)
                index[headers[i].Trim()] = i;
            return index;
        }
        private static MachineRecord ParseRow(string[] fields, Dictionary<string, int> index)
        {
            string Get(string col) => fields[index[col]].Trim();

            return new MachineRecord
            {
                Timestamp   = DateTime.ParseExact(Get("timestamp"), TimestampFormat, CultureInfo.InvariantCulture),
                MachineId   = Get("machineId"),
                Temperature = double.Parse(Get("temperature"), CultureInfo.InvariantCulture),
                Vibration   = double.Parse(Get("vibration"),   CultureInfo.InvariantCulture),
                Current     = double.Parse(Get("current"),     CultureInfo.InvariantCulture),
            };
        }

        public IEnumerator<MachineRecord> GetEnumerator() => _records.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public override string ToString() => $"CSVLoader(FilePath=\"{FilePath}\", RecordsLoaded={Count})";
    }
}