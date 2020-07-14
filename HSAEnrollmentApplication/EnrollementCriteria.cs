using System;
using System.Globalization;

namespace HSAEnrollmentApplication
{
    public class EnrollementCriteria
    {
        //ApplicationSubmissionDate: date given to compare submitted data against 
        public DateTime ApplicationSubmissionDate = System.DateTime.UtcNow.Date;
        // MinAgeRequirement: minimum age of applicant that is accepted
        public int MinAgeRequirement;
        // EffectiveDateRange:  maxium number of days application effective date is valid
        public int EffectiveDateRange;
        public DateTime TimeStamp = DateTime.UtcNow;

        ///<summary>
        /// Checks if the applicants date of birth is equal to or greater than the MinAgeRequirement given an ApplicationSubmissionDate
        ///</summary>
        public bool IsOfAge(string dateOfBirth)
        {
            try
            {
                // I do not know the legality of a leap year birthdays for this project I am considering them to be of age on non leap years on March 1st
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                DateTime dob = DateTime.ParseExact(dateOfBirth, "MMddYYYY", CultureInfo.InvariantCulture);

                if (dob.AddYears(MinAgeRequirement) >= ApplicationSubmissionDate)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown from IsOfAge method in EnrollementCriteria Class with " +
                    "ApplicationSubmissionDate [" + ApplicationSubmissionDate + "]" +
                    "MinAgeRequirement [" + MinAgeRequirement + "]" +
                    "Exception [" + e + "]" +
                    "at [" + TimeStamp + "]");

                throw;
            }
            


        }
    }
}
