using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pillpalbackend.Models.DTO
{
    public class DependentDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Birthday { get; set; }
        public string? Address { get; set; }
    }
}