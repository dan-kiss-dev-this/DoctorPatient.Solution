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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            _db.Doctors.Add(doctor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
    
}
