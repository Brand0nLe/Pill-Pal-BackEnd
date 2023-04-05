using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pillpalbackend.Models
{
    public class DependentModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Birthday { get; set; }
        public string? Address { get; set; }
        // public string[]? Allergies { get; set; }
        // public string[]? MedicalInfo { get; set; }
        // public string[]? DoctorInfo { get; set; }
        // public string[]? PharmInfo { get; set; }
        public DependentModel(){}
    }
}