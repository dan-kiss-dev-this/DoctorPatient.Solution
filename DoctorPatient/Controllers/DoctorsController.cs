using Microsoft.AspNetCore.Mvc;
using DoctorPatient.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorPatient.Controllers
{
    public class DoctorsController: Controller
    {
        private readonly DoctorPatientContext _db;
        public DoctorsController(DoctorPatientContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Doctor> model = _db.Doctors.ToList();
            return View(model);
        }
    }
    
}
