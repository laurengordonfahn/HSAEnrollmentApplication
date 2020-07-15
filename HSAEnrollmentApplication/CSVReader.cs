using System;
using FluentValidation;
using FluentValidation.Results;

namespace HSAEnrollmentApplication
{
    public class CSVReader
    {
        string CSVPath;
        public DateTime TimeStamp = DateTime.UtcNow;

        public CSVReader()
        {

            try
            {
                string[] rows = System.IO.File.ReadAllLines(CSVPath);
            
                foreach (string row in rows)
                {
                    string [] fields = row.Split(",");
                    EnrollmentDataModel enrollment = new EnrollmentDataModel().CreateInstanceFromList(fields);

                    //validate
                    EnrollmentDataValidator validator = new EnrollmentDataValidator();
                    ValidationResult result = validator.Validate(enrollment);
                    // read to memorystream
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to read in a csv file with " +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
            }
        }
    }
}
