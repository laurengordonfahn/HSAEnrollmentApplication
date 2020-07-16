using System;
using FluentValidation;
using System.Globalization;

namespace HSAEnrollmentApplication
{
    public class EnrollmentDataValidator : AbstractValidator<EnrollmentDataModel>
    {
        public EnrollmentDataValidator()
        {
            RuleFor(dataRow => dataRow.FirstName).NotEmpty();
            RuleFor(dataRow => dataRow.LastName).NotEmpty();
            RuleFor(dataRow => dataRow.DOB).NotEmpty()
                .Must(dob => IsValidDate(dob));
            RuleFor(dataRow => dataRow.PlanType).NotEmpty()
                .IsEnumName(typeof(PlanType));
            RuleFor(dataRow => dataRow.EffectiveDate).NotEmpty()
                .Must(IsValidDate);
        }

        public bool IsValidDate(string date)
        {

            DateTime dateOutput;
            string format = "MMddyyyy";
            Console.WriteLine("date", date, DateTime.TryParseExact(date, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput));
            return DateTime.TryParseExact(date, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput);
   
        }


        public bool IsValidPlanType(string planType)
        {
            Console.WriteLine("planType", planType, Enum.IsDefined(typeof(PlanType), planType));
            return Enum.IsDefined(typeof(PlanType), planType);

        }
    }
}
