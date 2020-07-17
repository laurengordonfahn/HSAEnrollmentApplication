using System.Collections.Generic;

namespace HSAEnrollmentApplication
{
    public class EnrollmentDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string PlanType { get; set; }
        public string EffectiveDate { get; set; }

        public EnrollmentDataModel (List<string> dataRow )
        {
            FirstName = dataRow[0];
            LastName = dataRow[1];
            DOB = dataRow[2];
            PlanType = dataRow[3];
            EffectiveDate = dataRow[4];
        }
    }
}
