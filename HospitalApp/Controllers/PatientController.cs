using Microsoft.AspNetCore.Mvc;
using HospitalApp.Models;
namespace HospitalApp.Controllers;
public class PatientController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(Patient patient)
    {
        return View("Details", patient);
    }
}