// id, name, birthdate, relationship to doctorpatientjoin
using System.Collections.Generic;
namespace DoctorPatient.Models
{
  public class Patient
  {
    public int PatientId {get; set; }
    public string Name { get; set; }
    public string Birthdate { get; set; }
    public List<DoctorPatientJoin> JoinEntities {get; }
  }
}