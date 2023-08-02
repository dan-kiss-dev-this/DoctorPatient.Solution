using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DoctorPatient.Models;

namespace DoctorPatient.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public ActionResult Index()
    {
        return View();
    }
}
