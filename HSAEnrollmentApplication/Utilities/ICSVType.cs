using System;
using System.Collections.Generic;
using System.Data;

namespace HSAEnrollmentApplication.Utilities
{
    public interface ICSVType
    {
        Response ValidateInitialDataRow(List<string> fields);
        List<string> ValidateCriteria(List<string> fields, DateTime processDate);
        void WriteToDataTable(DataTable table, List<string> fields);
    }
}
