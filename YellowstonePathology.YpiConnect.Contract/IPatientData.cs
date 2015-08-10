using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Contract
{
    public interface IPatientData
    {
        string PFirstName { get; set; }
        string PLastName { get; set; }
        string PMiddleInitial { get; set; }
        Nullable<DateTime> PBirthdate { get; set; }
        string PatientDisplayName { get; }
    }
}
