using System;
using System.Collections.Generic;
using System.Data;

namespace HSAEnrollmentApplication
{
    public interface ICSVReader
    {
        Response ValidateCSVData();
        List<string> ValidateEnrollmentCriteria(List<string> fields);
        Response ValidateInitialDataRow(List<string> fields);
        void WriteToDataTable(List<string> fields);
        void AddParams(string cSVPath, DataTable table, DateTime processDate);
    }
}