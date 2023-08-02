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

    


  }
}