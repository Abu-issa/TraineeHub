using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.Appliction.nterfaces
{
    public interface IExportService
    {
        byte[] ExportToExcel<T>(List<T> data, string sheetName);
        byte[] ExportToCsv<T>(List<T> data);
    }
}
