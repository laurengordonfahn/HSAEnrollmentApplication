using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using FluentValidation.Results;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;

namespace HSAEnrollmentApplication
{
    public class CSVReader
    {
        public string CSVPath;
        public DateTime TimeStamp = DateTime.UtcNow;

        //public CultureInfo CultureInvariant { get; private set; }

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

                        
                        string[] fields = row.Split(",");
                        EnrollmentDataModel enrollmentRow = new EnrollmentDataModel().CreateInstanceFromList(fields);
                        //validate
                        EnrollmentDataValidator validator = new EnrollmentDataValidator();
                        ValidationResult results = validator.Validate(enrollmentRow);
                        if (!results.IsValid)
                        {
                            response = new Response(false, "Data failed to validate" + JsonSerializer.Serialize(results.Errors));
                            reader.Close();
                            return response;

                        }
                   
                        
                        
                    }
                    reader.Close();
                }
                response = new Response(true, "Validation Complete");
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to read in a csv file for validation with " +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }
        }

        public Response ReadCSVToMemory()
        {

            try
            {
                Response response;


                using (MemoryStream ms = new MemoryStream())
                using (FileStream file = new FileStream(CSVPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    ms.Write(bytes, 0, (int)file.Length);
                    //MemoryStream memStream = new MemoryStream();
                    //int i = 0;
                    //while (!reader.EndOfStream)
                    //{
                    //    byte[] row = Encoding.ASCII.GetBytes(reader.ReadLine());
                    //    Console.WriteLine("rowLenght"+ row + row.Length);
                    //    long readerLength = reader.BaseStream.Length;
                    //    memStream.Write(row, i, (int)readerLength);
                    //    Console.WriteLine(memStream + memStream.Length.ToString());
                    //    i += row.Length;
                    //}
                    //reader.Close();
                    Console.WriteLine(ms + ms.Length.ToString());
                    response = new Response(true, "Successfully wrote file to Memory", ms);
                    return response;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to read in a csv file to Memory with " +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }
        }
    }
}
