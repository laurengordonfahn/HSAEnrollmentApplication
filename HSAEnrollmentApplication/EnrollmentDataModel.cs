using System;

namespace HSAEnrollmentApplication
{
    public class EnrollmentDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DOB { get; set; }
        public PlanType PlanType { get; set; }
        public int EffectiveDate { get; set; }
    }
}
