// doctorid, partientid, doctorpatientjoinid
namespace DoctorPatient.Models
{
  public class DoctorPatientJoin
  {
    public int DoctorPatientJoinId {get; set;}
    public int PatientId {get; set; }
    public Patient Patient {get; set;}
    public int DoctorId {get; set; }
    public Doctor Doctor {get; set;}
  }
}