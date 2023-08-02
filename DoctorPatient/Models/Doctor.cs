
// name, specialy, and doctorid referecne to doctor patient table
using System.Collections.Generic;

namespace DoctorPatient.Models
{
    public class Doctor
    {
        public int DoctorId {get; set;}
        public string Name { get; set;}
        public string Specialty {get; set;}
        public List<DoctorPatientJoin> JoinEntities {get;}
    }
}