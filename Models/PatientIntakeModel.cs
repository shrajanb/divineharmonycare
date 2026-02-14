using System.ComponentModel.DataAnnotations;

namespace DivineHarmonyCare.Models;

public class PatientIntakeModel
{
    public string? Id { get; set; }
    public DateTime SubmittedDate { get; set; } = DateTime.Now;

    // 1. Patient Identification
    [Required(ErrorMessage = "Full name is required")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of birth is required")]
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    [Display(Name = "Alberta Personal Health Number (PHN)")]
    public string? HealthNumber { get; set; }

    [Display(Name = "Current Address")]
    public string? CurrentAddress { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Display(Name = "Primary Phone")]
    [Phone]
    public string PrimaryPhone { get; set; } = string.Empty;

    [Display(Name = "Email Address")]
    [EmailAddress]
    public string? EmailAddress { get; set; }

    // 2. Responsible Party / Legal Representative
    [Display(Name = "Representative Name")]
    public string? RepresentativeName { get; set; }

    [Display(Name = "Relationship to Patient")]
    public string? RepresentativeRelationship { get; set; }

    [Display(Name = "Representative Phone")]
    [Phone]
    public string? RepresentativePhone { get; set; }

    [Display(Name = "Representative Email")]
    [EmailAddress]
    public string? RepresentativeEmail { get; set; }

    [Display(Name = "Legal Authority")]
    public string? LegalAuthority { get; set; } // PowerOfAttorney, Guardian, Trusteeship, None

    // 3. Medical & Clinical Information
    [Display(Name = "Primary Physician")]
    public string? PrimaryPhysician { get; set; }

    [Display(Name = "Physician Phone/Fax")]
    public string? PhysicianPhone { get; set; }

    [Display(Name = "Primary Diagnoses")]
    public string? PrimaryDiagnoses { get; set; }

    [Display(Name = "Known Allergies")]
    public string? KnownAllergies { get; set; }

    [Display(Name = "Dietary Restrictions")]
    public string? DietaryRestrictions { get; set; }

    [Display(Name = "Personal Directive")]
    public bool PersonalDirective { get; set; }

    [Display(Name = "Goals of Care Designation (GCD)")]
    public bool GoalsOfCareDesignation { get; set; }

    [Display(Name = "Requires Medication Assistance")]
    public bool RequiresMedicationAssistance { get; set; }

    [Display(Name = "Medication Method")]
    public string? MedicationMethod { get; set; } // Reminders, PhysicalAdministration

    // 4. Care Needs Assessment - Personal Care (ADLs)
    public bool BathingShowering { get; set; }
    public bool DressingGrooming { get; set; }
    public bool OralHygiene { get; set; }
    public bool ToiletingIncontinenceCare { get; set; }
    public bool Transfers { get; set; }
    public bool RangeOfMotionExercises { get; set; }

    // Household & Companionship (IADLs)
    public bool MealPreparation { get; set; }
    public bool LightHousekeeping { get; set; }
    public bool Laundry { get; set; }
    public bool TransportationAppointments { get; set; }
    public bool CompanionshipSocialization { get; set; }
    public bool PetCareAssistance { get; set; }

    // Cognitive & Safety
    public bool DementiaAlzheimersCare { get; set; }
    public bool FallRisk { get; set; }
    public bool FallHistory { get; set; }
    public bool WanderingRisk { get; set; }
    public bool Requires24HourSupervision { get; set; }
    public bool BehaviorManagement { get; set; }

    // 5. Scheduling & Logistics
    [Display(Name = "Requested Start Date")]
    [DataType(DataType.Date)]
    public DateTime? RequestedStartDate { get; set; }

    [Display(Name = "Desired Schedule")]
    public string? DesiredSchedule { get; set; } // LiveIn, Hourly

    [Display(Name = "Specific Hours Needed")]
    public string? SpecificHoursNeeded { get; set; }

    [Display(Name = "Number of Days per Week")]
    public int? DaysPerWeek { get; set; }

    // 6. Home Environment & Safety
    [Display(Name = "Pets in the Home")]
    public bool PetsInHome { get; set; }

    [Display(Name = "Pet Details")]
    public string? PetDetails { get; set; }

    [Display(Name = "Smoking Household")]
    public bool SmokingHousehold { get; set; }

    [Display(Name = "Medical Equipment in Use")]
    public string? MedicalEquipment { get; set; }

    // 7. Consent & Signature
    [Display(Name = "Printed Name")]
    public string? ConsentPrintedName { get; set; }

    [Display(Name = "Signature")]
    public string? SignatureData { get; set; } // Base64 encoded signature image

    [Display(Name = "Date")]
    [DataType(DataType.Date)]
    public DateTime? ConsentDate { get; set; }
}
