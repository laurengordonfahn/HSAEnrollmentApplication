using System;
using FluentValidation.Results;
using System.Text.Json;
using System.Collections.Generic;
using System.Data;
using HSAEnrollmentApplication.Models;
using System.Globalization;

namespace HSAEnrollmentApplication.Utilities
{
    public class EnrollmentCSV
    {
        public DateTime TimeStamp = DateTime.UtcNow;
        public string CSVPath;
        public DataTable Table;
        public DateTime ProcessDate;

        public EnrollmentCSV()
        {
        }

        /// <summary>
        /// Does intial data validation
        /// </summary
        public Response ValidateInitialDataRow(List<string> fields)
        {
            try
            {
                EnrollmentDataModel enrollmentRow = new EnrollmentDataModel(fields);
                
                EnrollmentDataValidator validator = new EnrollmentDataValidator();
                ValidationResult results = validator.Validate(enrollmentRow);

                if (!results.IsValid)
                {
                    return new Response(false, "Data failed to validate" + JsonSerializer.Serialize(results.Errors));
                }
                return new Response(true, "Data validated");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to validate data row with " +
                   "Row [" + JsonSerializer.Serialize(fields) + "]" +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }

        }

        /// <summary>
        /// Accesses valid data for Enrollment status acceptablity
        /// </summary
        public List<string> ValidateCriteria(List<string> fields)
        {
            try
            {
                EnrollmentDataModel enrollmentRow = new EnrollmentDataModel(fields);
                //validate
                EnrollmentAssessmentValidator validator = new EnrollmentAssessmentValidator();
                validator.ProcessDate = ProcessDate;
                ValidationResult results = validator.Validate(enrollmentRow);
                if (!results.IsValid)
                {
                    fields.Add("Rejected");
                }
                else
                {
                    fields.Add("Accepted");
                }
                return fields;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to assess data row with " +
                   "Row [" + JsonSerializer.Serialize(fields) + "]" +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }

        }

        /// <summary>
        /// Writes a row of data to the data table in memory
        /// </summary
        public void WriteToDataTable(List<string> fields)
        {
            try
            {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                DateTime dob = DateTime.ParseExact(fields[2], "MMddyyyy", CultureInfo.InvariantCulture);
                DateTime effectiveDate = DateTime.ParseExact(fields[4], "MMddyyyy", CultureInfo.InvariantCulture);

                string shortDOB = dob.ToShortDateString();
                string shortEffectiveDate = effectiveDate.ToShortDateString();
                AssessmentStatus status = (AssessmentStatus)Enum.Parse(typeof(AssessmentStatus), fields[5]);
                PlanType plan = (PlanType)Enum.Parse(typeof(PlanType), fields[3]);

                Table.Rows.Add(status, fields[0], fields[1], shortDOB, plan, shortEffectiveDate);
                Table.AcceptChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to write data row to table with " +
                   "Row [" + JsonSerializer.Serialize(fields) + "]" +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }
        }
    }
}
