using System.Collections.Generic;

namespace HSAEnrollmentApplication.Models
{
    public class EnrollmentAssessmentDataModel
    {
     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public PlanType PlanType { get; set; }
        public string EffectiveDate { get; set; }

        public EnrollmentAssessmentDataModel(List<string> dataRow)
        {
            DOB = dataRow[2];
            EffectiveDate = dataRow[4];
        }
        
    }
}

