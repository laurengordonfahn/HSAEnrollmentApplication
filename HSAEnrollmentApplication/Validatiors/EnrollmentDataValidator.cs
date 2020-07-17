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
                .Matches(@"^[0-9]{8}$")
                .Must(dob => IsValidDate(dob));
            RuleFor(dataRow => dataRow.PlanType).NotEmpty()
                .IsEnumName(typeof(PlanType));
            RuleFor(dataRow => dataRow.EffectiveDate).NotEmpty()
                .Matches(@"^[0-9]{8}$")
                .Must(IsValidDate);
        }

        public bool IsValidDate(string date)
        {
            DateTime dateOutput;
            string format = "MMddyyyy";
            return DateTime.TryParseExact(date, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput);
        }


        public bool IsValidPlanType(string planType)
        {
            return Enum.IsDefined(typeof(PlanType), planType);
        }
    }
}
