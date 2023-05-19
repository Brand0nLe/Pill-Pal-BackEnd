using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pillpalbackend.Models.DTO
{
    public class CreateAccountDTO
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}