using System;
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

        public EnrollmentDataModel CreateInstanceFromList(string [] dataRow )
        {
            EnrollmentDataModel enrollment = new EnrollmentDataModel();
            enrollment.FirstName = dataRow[0];
            enrollment.LastName = dataRow[1];
            enrollment.DOB = dataRow[2];
            enrollment.PlanType = dataRow[3];
            enrollment.EffectiveDate = dataRow[4];

            return enrollment;
        }
    }
}
