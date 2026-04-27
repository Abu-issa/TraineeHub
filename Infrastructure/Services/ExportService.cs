using System;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Appliction.nterfaces;

namespace TraineeHub.Infrastructure.Services
{
    public class ExportService : IExportService
    {
        public byte[] ExportToExcel<T>(List<T> data, string sheetName)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            var properties = typeof(T).GetProperties();

            // Header
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
            }

            // Data
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < properties.Length; j++)
                {
                    var value = properties[j].GetValue(data[i]);
                    worksheet.Cell(i + 2, j + 1).Value = value?.ToString();
                }
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] ExportToCsv<T>(List<T> data)
        {
            var sb = new StringBuilder();
            var properties = typeof(T).GetProperties();

            sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));

            foreach (var item in data)
            {
                var values = properties.Select(p =>
                    $"\"{p.GetValue(item)?.ToString()?.Replace("\"", "\"\"")}\"");

                sb.AppendLine(string.Join(",", values));
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
