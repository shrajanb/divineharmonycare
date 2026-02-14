using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using DivineHarmonyCare.Models;

namespace DivineHarmonyCare.Controllers;

public class InvoiceController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly string _dataPath;

    public InvoiceController(IWebHostEnvironment env)
    {
        _env = env;
        _dataPath = Path.Combine(_env.WebRootPath, "data", "invoices");
        if (!Directory.Exists(_dataPath))
            Directory.CreateDirectory(_dataPath);
    }

    public IActionResult Create()
    {
        var model = new InvoiceModel
        {
            InvoiceNumber = $"INV-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..4].ToUpper()}",
            LineItems = new List<InvoiceLineItem>
            {
                new InvoiceLineItem { Date = DateTime.Today }
            }
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(InvoiceModel model)
    {
        // Recalculate totals
        model.Subtotal = model.LineItems?.Sum(li => li.Hours * li.Rate) ?? 0;
        model.TotalDue = model.Subtotal + model.Tax;

        model.Id = Guid.NewGuid().ToString("N")[..8].ToUpper();
        model.CreatedDate = DateTime.Now;

        if (string.IsNullOrEmpty(model.InvoiceNumber))
            model.InvoiceNumber = $"INV-{DateTime.Now:yyyyMMdd}-{model.Id[..4]}";

        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(model, options);
        var fileName = $"{model.Id}_{model.ClientName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}.json";
        var filePath = Path.Combine(_dataPath, fileName);
        System.IO.File.WriteAllText(filePath, json);

        TempData["SuccessMessage"] = $"Invoice created successfully! Invoice #: {model.InvoiceNumber}";
        return RedirectToAction("View_", new { id = model.Id });
    }

    public IActionResult View_(string id)
    {
        var file = Directory.GetFiles(_dataPath, $"{id}_*").FirstOrDefault();
        if (file == null) return NotFound();

        var json = System.IO.File.ReadAllText(file);
        var model = JsonSerializer.Deserialize<InvoiceModel>(json);
        return View("ViewInvoice", model);
    }

    public IActionResult Search(string? q)
    {
        var searchModel = new InvoiceSearchModel { SearchTerm = q };

        var files = Directory.GetFiles(_dataPath, "*.json");
        foreach (var file in files.OrderByDescending(f => System.IO.File.GetCreationTime(f)))
        {
            var json = System.IO.File.ReadAllText(file);
            var invoice = JsonSerializer.Deserialize<InvoiceModel>(json);
            if (invoice == null) continue;

            if (string.IsNullOrWhiteSpace(q) ||
                invoice.ClientName.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                invoice.InvoiceNumber?.Contains(q, StringComparison.OrdinalIgnoreCase) == true ||
                invoice.Id?.Contains(q, StringComparison.OrdinalIgnoreCase) == true)
            {
                searchModel.Results.Add(invoice);
            }
        }

        return View(searchModel);
    }
}
