using Microsoft.AspNetCore.Mvc;
using HospitalApp.Models;
namespace HospitalApp.Controllers;
public class DoctorController : Controller
{
    public IActionResult Diagnose()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Diagnose(Doctor doctor)
    {
        return View("Result", doctor);
    }
}