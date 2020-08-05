using System;
using System.Data;

namespace HSAEnrollmentApplication.Utilities
{
    public interface IReadCSV
    {
        Response ProcessDataByRow(string csvPath, DateTime processDate, DataTable table);
    }
}
