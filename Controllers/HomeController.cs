using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceTracker.Domain.Repository.Abstractions;
using ServiceTracker.Models;
using System.Diagnostics;

namespace ServiceTracker.Controllers;

[Authorize]
public class HomeController(IRecordRepository dataManager) : Controller
{
    public IActionResult Index()
    {
        return View(dataManager.GetAllRecordsAsync());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
