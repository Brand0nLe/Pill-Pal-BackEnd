using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pillpalbackend.Models
{
    public class MedicationModel
    {
        public int Id { get; set; }
        public string MedicationName { get; set; }
        public string? DosageStrength { get; set; }
        public string? DosageQuantity { get; set; }
        public string? MedicationDirection { get; set; }
        public int MedicationQuantity { get; set; }
        public int RefillQuantity { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? MedicationReason { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorContact { get; set; }
        public string? PharmacyName { get; set; }
        public string? PharmacyLocation { get; set; }
        public string? PharmacyContact { get; set; }
        public string? SideEffects { get; set; }
        public string? Notes { get; set; }
        public bool MedsLeft { get; set; }
        public bool MedsLeftReminder { get; set; }
        public bool Deleted { get; set; }
        public MedicationModel(){}
    }
}