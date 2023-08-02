using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DoctorPatient.Models;
using System.Collections.Generic;
using System.Linq;

namespace DoctorPatient.Controllers
{
  public class PatientsController : Controller
  {
    private readonly DoctorPatientContext _db;

    public PatientsController(DoctorPatientContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Patient> model = _db.Patients.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patient patient)
    {
      _db.Patients.Add(patient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Patient foundPatient = _db.Patients
          .Include(patient => patient.JoinEntities)
          .ThenInclude(join => join.Doctor)
          .FirstOrDefault(patient => patient.PatientId == id);
      return View(foundPatient);
    }

    public ActionResult AddDoctor(int id)
    {
      Patient foundPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      // List<Doctor> allDoctors = _db.Doctors.ToList();
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View(foundPatient);
    }

    [HttpPost]
    public ActionResult AddDoctor(Patient patient, int doctorId)
    {
#nullable enable
      DoctorPatientJoin? joinEntity = _db.DoctorPatientJoins.FirstOrDefault(join => (join.DoctorId == doctorId && join.PatientId == patient.PatientId));
#nullable disable
      if (joinEntity == null && doctorId != 0)
      {
        _db.DoctorPatientJoins.Add(new DoctorPatientJoin() { DoctorId = doctorId, PatientId = patient.PatientId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = patient.PatientId });
    }

    //delete the doctor for a patient with a button
    [HttpPost]
    public ActionResult DeleteJoin(int doctorPatientJoinId)
    {
      // we do not remove the doctor itself, we remove the join relationship
      DoctorPatientJoin doctorPatientRelationToRemove = _db.DoctorPatientJoins.FirstOrDefault(entry => entry.DoctorPatientJoinId == doctorPatientJoinId);
      int patientId = doctorPatientRelationToRemove.PatientId;
      _db.DoctorPatientJoins.Remove(doctorPatientRelationToRemove);
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = patientId});
    }



  }
}