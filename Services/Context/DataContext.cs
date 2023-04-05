using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pillpalbackend.Services.Context
{
    public class DataContext : DBContext
    {
        public DirectoryBrowserExtensions<UserModel> UserInfo { get; set;}
        public DirectoryBrowserExtensions<DependentModel> DependentInfo { get; set;}
        public DirectoryBrowserExtensions<MedicationModel> MedicationInfo { get; set;}

        public DataContext(DbContextOptions options): base(options)
        {}

        protected override void OnModelCreating(ModelsBuilder builder){
            base.OnModelCreating(builder);
        }
    }
}