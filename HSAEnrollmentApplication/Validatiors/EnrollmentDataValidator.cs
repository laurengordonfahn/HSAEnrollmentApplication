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
                .Must(planType => IsValidPlanType(planType));
            RuleFor(dataRow => dataRow.EffectiveDate).NotEmpty()
                .Must(effectiveDate => IsValidDate(effectiveDate));
        }

        private bool IsValidDate(string date)
        {

            DateTime dateOutput;
            string format = "MMddyyyy";

            return DateTime.TryParseExact(date, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput);
   
        }


        private bool IsValidPlanType(string planType)
        {

           return Enum.IsDefined(typeof(PlanType), planType);

        }
    }
}
