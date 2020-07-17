using System;
using FluentValidation;
using System.Globalization;

namespace HSAEnrollmentApplication
{
    public class EnrollmentAssessmentValidator : AbstractValidator<EnrollmentDataModel>
    {
        //ApplicationSubmissionDate: date given to compare submitted data against 
        public DateTime ApplicationSubmissionDate = System.DateTime.UtcNow.Date;
        // MinAgeRequirement: minimum age of applicant that is accepted
        public int MinAgeRequirement;
        // EffectiveDateRange:  maxium number of days application effective date is valid
        public int EffectiveDateRange;

        public EnrollmentAssessmentValidator()
        {
            RuleFor(dataRow => dataRow.DOB).NotEmpty()
                .Must(dob => IsEighteenPlus(dob));
            RuleFor(dataRow => dataRow.EffectiveDate).NotEmpty()
                .Must(IsStartWithInRange);
        }

        public bool IsEighteenPlus(string dob)
        {

            // I do not know the legality of a leap year birthdays for this project I am considering them to be of age on non leap years on March 1st
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            DateTime date = DateTime.ParseExact(dob, "MMddyyyy", CultureInfo.InvariantCulture);
            Console.WriteLine("dob" + date.AddYears(MinAgeRequirement) + ApplicationSubmissionDate);
            if (date.AddYears(MinAgeRequirement) <= ApplicationSubmissionDate)
            {
                return true;
            }
            return false;

        }


        public bool IsStartWithInRange(string dateToTakeEffect)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            DateTime effectivDate = DateTime.ParseExact(dateToTakeEffect, "MMddyyyy", CultureInfo.InvariantCulture);
            Console.WriteLine("effective" + ApplicationSubmissionDate.AddDays(EffectiveDateRange) + effectivDate);
            if (ApplicationSubmissionDate.AddDays(EffectiveDateRange) >= effectivDate)
            {
                return true;
            }
            return false;
        }
    }
}

