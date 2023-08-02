using Microsoft.AspNetCore.Mvc;
using DoctorPatient.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorPatient.Controllers
{
  public class DoctorsController : Controller
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

    public ActionResult Details(int id)
    {
      Doctor doctorModel = _db.Doctors
                  .Include(doctor => doctor.JoinEntities)
                  .ThenInclude(join => join.Patient)
                  .FirstOrDefault(doctor => doctor.DoctorId == id);
      return View(doctorModel);
    }

    public ActionResult AddPatient(int id)
    {
      Doctor doctorModel = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
      Console.WriteLine(4949);
      Console.WriteLine(doctorModel);
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "Name");
      return View(doctorModel);
    }

    [HttpPost]
    public ActionResult AddPatient(Doctor doctor, int patientId)
    {
#nullable enable
      DoctorPatientJoin? joinEntity = _db.DoctorPatientJoins.FirstOrDefault(jointEntry => (jointEntry.DoctorId == doctor.DoctorId && jointEntry.PatientId == patientId));
#nullable disable
      if (joinEntity == null && patientId != 0)
      {
        _db.DoctorPatientJoins.Add(new DoctorPatientJoin() { DoctorId = doctor.DoctorId, PatientId = patientId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = doctor.DoctorId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      DoctorPatientJoin joinEntry = _db.DoctorPatientJoins.FirstOrDefault(entry => entry.DoctorPatientJoinId == joinId);
      int doctorId = joinEntry.DoctorId;
      _db.DoctorPatientJoins.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = doctorId });
    }

    public ActionResult Edit(int id)
    {
      Doctor editDoctor = _db.Doctors.FirstOrDefault(entry => entry.DoctorId == id);
      return View(editDoctor);
    }

    [HttpPost]
    public ActionResult Edit(Doctor doctor)
    {
      _db.Doctors.Update(doctor);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = doctor.DoctorId });
    }
  }

}
