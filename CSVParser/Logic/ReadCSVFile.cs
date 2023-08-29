using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CSVParser.Model;

namespace CSVParser.Logic
{
    public class ReadCSVFile
    {
        public List<T> ReadRecords<T>(string csvFilePath)
        {
            List<T> records = new List<T>();

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                records = csv.GetRecords<T>().ToList();
            }

            return records;
        }
    }
}
