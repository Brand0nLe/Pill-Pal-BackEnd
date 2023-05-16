using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pillpalbackend.Models;
using Microsoft.EntityFrameworkCore;

namespace pillpalbackend.Services.Context
{
    public class DataContext : DbContext
    {
        public DbSet<UserModel> UserInfo { get; set;}
        public DbSet<DependentModel> DependentInfo { get; set;}
        public DbSet<MedicationModel> MedicationInfo { get; set;}

        public DataContext(DbContextOptions options): base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
        }
    }
}