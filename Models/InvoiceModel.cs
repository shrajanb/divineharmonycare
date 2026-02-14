using System.ComponentModel.DataAnnotations;

namespace DivineHarmonyCare.Models;

public class InvoiceModel
{
    public string? Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // Client Info
    [Required(ErrorMessage = "Client name is required")]
    [Display(Name = "Client Name")]
    public string ClientName { get; set; } = string.Empty;

    [Display(Name = "Invoice #")]
    public string? InvoiceNumber { get; set; }

    [Display(Name = "Service Address")]
    public string? ServiceAddress { get; set; }

    [Required]
    [Display(Name = "Invoice Date")]
    [DataType(DataType.Date)]
    public DateTime InvoiceDate { get; set; } = DateTime.Now;

    [Required]
    [Display(Name = "Due Date")]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(30);

    [Display(Name = "Phone")]
    public string? Phone { get; set; }

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Caregiver Name")]
    public string? CaregiverName { get; set; }

    // Line Items
    public List<InvoiceLineItem> LineItems { get; set; } = new();

    // Totals
    [Display(Name = "Subtotal")]
    public decimal Subtotal { get; set; }

    [Display(Name = "Tax (if applicable)")]
    public decimal Tax { get; set; }

    [Display(Name = "Total Due")]
    public decimal TotalDue { get; set; }

    // Payment
    [Display(Name = "Payment Method")]
    public string? PaymentMethod { get; set; } // CreditCard, ETransfer, Cash, Cheque

    [Display(Name = "Notes / Terms")]
    public string? Notes { get; set; }

    // Signature
    [Display(Name = "Authorized Signature")]
    public string? SignatureData { get; set; }

    [Display(Name = "Signature Date")]
    [DataType(DataType.Date)]
    public DateTime? SignatureDate { get; set; }
}

public class InvoiceLineItem
{
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Display(Name = "Service Description")]
    public string ServiceDescription { get; set; } = string.Empty;

    public decimal Hours { get; set; }

    public decimal Rate { get; set; }

    public decimal Amount => Hours * Rate;
}

public class InvoiceSearchModel
{
    public string? SearchTerm { get; set; }
    public List<InvoiceModel> Results { get; set; } = new();
}

public class IntakeSearchModel
{
    public string? SearchTerm { get; set; }
    public List<PatientIntakeModel> Results { get; set; } = new();
}
