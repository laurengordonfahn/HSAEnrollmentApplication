using System.Collections.Generic;

namespace HSAEnrollmentApplication
{
    public class DateModel : DateFormatModel
    {
        public DateFormatModel Info {get; set; }
        
        public DateModel(DateFormatModel dateFormat)
        {
            Info = dateFormat;
        }
    }

    public class DateFormatModel
    {
        public string Date { get; set; }
        public string Format { get; set; }

        public DateFormatModel(string date, string format)
        {
            Date = date;
            Format = format;
        }
    }
}
