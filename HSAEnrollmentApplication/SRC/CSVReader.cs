using System;
using System.IO;
using FluentValidation.Results;
using System.Text.Json;
using System.Collections.Generic;
using System.Data;
using HSAEnrollmentApplication.Models;
using System.Globalization;

namespace HSAEnrollmentApplication
{
    public class CSVReader
    {
        public string CSVPath { get; set; }
        public DataTable Table;
        //ApplicationSubmissionDate: date given to compare submitted data against 
        public DateTime ApplicationSubmissionDate { get; set; }
        public DateTime TimeStamp = DateTime.UtcNow;

        public CSVReader(string csvPath, DataTable table, DateTime processDate)
        {
            CSVPath = csvPath;
            Table = table;
            ApplicationSubmissionDate = processDate;
        }


        public Response ValidateCSVData()
        {

            try
            {
                Response response;
                using (StreamReader reader = new StreamReader(File.OpenRead(CSVPath)))
                {

                    while (!reader.EndOfStream)
                    {
                        string row = reader.ReadLine();

                        List<string> fields = new List<string>(row.Split(","));

                        //validate initial
                        Response result = ValidateInitialDataRow(fields);
                        if (!result.Success)
                        {
                            response = new Response(false, "A record in the file failed validation.  Processing has stopped.");
                            reader.Close();
                            Table.Clear();
                            return result;
                        }

                        //assessment
                        List<string> assessedRow = ValidateEnrollmentCriteria(fields);

                        //read to table
                        WriteToDataTable(fields);

                    }
                    reader.Close();
                }
                response = new Response(true, "Validation Complete successfully wrote data to table in Memory");
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to read in a process csv data with " +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }
        }

        public Response ValidateInitialDataRow(List<string> fields)
        {
            try
            {
                EnrollmentDataModel enrollmentRow = new EnrollmentDataModel(fields);
                //validate
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
                Console.WriteLine("Exception thrown trying to validation data row with " +
                   "Row [" + JsonSerializer.Serialize(fields) + "]" +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }

        }

        public List<string> ValidateEnrollmentCriteria(List<string> fields)
        {
            try
            {
                EnrollmentDataModel enrollmentRow = new EnrollmentDataModel(fields);
                //validate
                EnrollmentAssessmentValidator validator = new EnrollmentAssessmentValidator();
                validator.ApplicationSubmissionDate = ApplicationSubmissionDate;
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
