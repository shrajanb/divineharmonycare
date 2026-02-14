using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using DivineHarmonyCare.Models;

namespace DivineHarmonyCare.Controllers;

public class IntakeController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly string _dataPath;

    public IntakeController(IWebHostEnvironment env)
    {
        _env = env;
        _dataPath = Path.Combine(_env.WebRootPath, "data", "intakes");
        if (!Directory.Exists(_dataPath))
            Directory.CreateDirectory(_dataPath);
    }

    public IActionResult Form()
    {
        return View(new PatientIntakeModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Form(PatientIntakeModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Id = Guid.NewGuid().ToString("N")[..8].ToUpper();
        model.SubmittedDate = DateTime.Now;

        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(model, options);
        var fileName = $"{model.Id}_{model.FullName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}.json";
        var filePath = Path.Combine(_dataPath, fileName);
        System.IO.File.WriteAllText(filePath, json);

        TempData["SuccessMessage"] = $"Patient intake form submitted successfully! Reference ID: {model.Id}";
        return RedirectToAction("Confirmation", new { id = model.Id });
    }

    public IActionResult Confirmation(string id)
    {
        var file = Directory.GetFiles(_dataPath, $"{id}_*").FirstOrDefault();
        if (file == null) return NotFound();

        var json = System.IO.File.ReadAllText(file);
        var model = JsonSerializer.Deserialize<PatientIntakeModel>(json);
        return View(model);
    }

    public IActionResult Search(string? q)
    {
        var searchModel = new IntakeSearchModel { SearchTerm = q };

        if (!string.IsNullOrWhiteSpace(q))
        {
            var files = Directory.GetFiles(_dataPath, "*.json");
            foreach (var file in files)
            {
                var json = System.IO.File.ReadAllText(file);
                var intake = JsonSerializer.Deserialize<PatientIntakeModel>(json);
                if (intake != null &&
                    (intake.FullName.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                     intake.Id?.Contains(q, StringComparison.OrdinalIgnoreCase) == true ||
                     intake.PrimaryPhone.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                     intake.EmailAddress?.Contains(q, StringComparison.OrdinalIgnoreCase) == true))
                {
                    searchModel.Results.Add(intake);
                }
            }
        }
        else
        {
            // Show all if no search term
            var files = Directory.GetFiles(_dataPath, "*.json");
            foreach (var file in files.OrderByDescending(f => System.IO.File.GetCreationTime(f)))
            {
                var json = System.IO.File.ReadAllText(file);
                var intake = JsonSerializer.Deserialize<PatientIntakeModel>(json);
                if (intake != null)
                    searchModel.Results.Add(intake);
            }
        }

        return View(searchModel);
    }

    public IActionResult View_(string id)
    {
        var file = Directory.GetFiles(_dataPath, $"{id}_*").FirstOrDefault();
        if (file == null) return NotFound();

        var json = System.IO.File.ReadAllText(file);
        var model = JsonSerializer.Deserialize<PatientIntakeModel>(json);
        return View("Confirmation", model);
    }
}
