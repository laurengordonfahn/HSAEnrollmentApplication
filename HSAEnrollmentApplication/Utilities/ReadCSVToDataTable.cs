using System;
using System.IO;
using FluentValidation.Results;
using System.Text.Json;
using System.Collections.Generic;
using System.Data;
using HSAEnrollmentApplication.Models;
using System.Globalization;

namespace HSAEnrollmentApplication.Utilities
{
    public class ReadCSVToDataTable
    {
        public DateTime TimeStamp = DateTime.UtcNow;
        public string CSVPath;
        public DataTable Table;
        public DateTime ProcessDate;

        public ReadCSVToDataTable()
        {
        }
    }
}
