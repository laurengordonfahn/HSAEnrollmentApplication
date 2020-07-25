using System;
using FluentValidation;
using System.Globalization;

namespace HSAEnrollmentApplication
{
    public class DateStringValidator
    {
        public Response ValidateDateStringInline(string date, string format = "MMddyyyy")
        {
            DateTime dateOutput;

            if (DateTime.TryParseExact(date, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput))
            {
                return new Response(true, "Date is valid for format", dateOutput);
            }
            else
            {
                return new Response(false, "The date you entered is either not a valid date or not in the format mmddyyyy, please try again or simply press the return/enter key to use the default GMT");
            }
        }
    }
    //public class DateStringValidator : AbstractValidator<DateModel>
    //{
    //    DateTime DateOutput;
    //    public DateStringValidator()
    //    {
    //        RuleFor(info => info.Date).NotEmpty();
    //        RuleFor(info => info.Format).NotEmpty();
    //        RuleFor(info => info).NotEmpty()
    //            .Must(IsValidDate).WithState(info => DateOutput);
    //    }

    //    public bool IsValidDate(DateModel info)
    //    {
    //        DateTime dateOutput;
    //        bool isTrue = DateTime.TryParseExact(info.Date, info.Format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput);
    //        DateOutput = dateOutput;
    //        return isTrue;
    //    }

    //}
}

