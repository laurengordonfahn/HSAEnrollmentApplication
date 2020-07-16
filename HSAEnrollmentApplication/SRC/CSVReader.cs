using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using FluentValidation.Results;

namespace HSAEnrollmentApplication
{
    public class CSVReader
    {
        public string CSVPath;
        public DateTime TimeStamp = DateTime.UtcNow;

        public CultureInfo CultureInvariant { get; private set; }

        public Response ReadCSVToMemoryStream()
        {

            try
            {
                Response response;
                using (StreamReader reader = new StreamReader(File.OpenRead(CSVPath)))
                {
                    while (!reader.EndOfStream)
                    {
                        string row = reader.ReadLine();

                        if (!String.IsNullOrWhiteSpace(row))
                        {
                            string[] fields = row.Split(",");
                            EnrollmentDataModel enrollmentRow = new EnrollmentDataModel().CreateInstanceFromList(fields);
                            //validate
                            EnrollmentDataValidator validator = new EnrollmentDataValidator();
                            ValidationResult results = validator.Validate(enrollmentRow);
                            if (!results.IsValid)
                            {

                                foreach (var failure in results.Errors)
                                {
                                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                                }
                                response = new Response(false, "Data failed to validate");
                                reader.Close();
                                return response;

                            }
                            //read to memory stream
                            
                        }
                        
                    }
                    reader.Close();
                }
                // read to memorystream
                
                response = new Response(false, "Working on memstream");
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to read in a csv file with " +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }
        }
    }
}
