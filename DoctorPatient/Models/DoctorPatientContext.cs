using Microsoft.EntityFrameworkCore;

namespace DoctorPatient.Models
{
    public class DoctorPatientContext: DbContext
    {
        public DbSet<Doctor> Doctors {get; set;}
        public DbSet<Patient> Patients {get; set;}
        public DbSet<DoctorPatientJoin> DoctorPatientJoins {get; set;}
        
        public DoctorPatientContext(DbContextOptions options) : base(options) {}
    }
}