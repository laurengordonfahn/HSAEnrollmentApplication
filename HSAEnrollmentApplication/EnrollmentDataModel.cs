using System;

namespace HSAEnrollmentApplication
{
    public class EnrollmentDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public PlanType PlanType { get; set; }
        public string EffectiveDate { get; set; }
    }
}
