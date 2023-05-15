using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using pillpalbackend.Models;
using pillpalbackend.Models.DTO;
using pillpalbackend.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace pillpalbackend.Services
{
    public class PillPalService : ControllerBase
    {
        private const string BaseUrl = "https://api.fda.gov/drug/ndc.json?search=";

        private readonly DataContext _context;
        public PillPalService(DataContext context)
        {
            _context = context;
        }


        public async Task<MedicationDTO> SaveMedication(string medicationName)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(BaseUrl + medicationName);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var medication = new MedicationDTO(responseContent);
                    return medication;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool DoesMedicationExist(string? MedicationName)
        {

            return _context.MedicationInfo.SingleOrDefault(user => user.MedicationName == MedicationName) != null;

        }

        public object GetMedicationByUserId(int LookingUserId){
            return _context.MedicationInfo.Where(Medication => Medication.UserId == LookingUserId).Select(medication => new
        {
            medication.MedicationName,
            medication.DosageStrength
        }).ToList();
        }

        public bool AddMedication(MedicationDTO MedicationToAdd)
        {

            bool result = false;
            // if (!DoesMedicationExist(MedicationToAdd.MedicationName))
            
 
                MedicationModel newMedicine = new MedicationModel();
 
                newMedicine.Id = MedicationToAdd.Id;
                newMedicine.Test = MedicationToAdd.Test;
                newMedicine.UserId = MedicationToAdd.UserId;
                newMedicine.MedicationName = MedicationToAdd.MedicationName;
                newMedicine.DosageStrength = MedicationToAdd.DosageStrength;
                newMedicine.DosageQuantity = MedicationToAdd.DosageQuantity;
                newMedicine.MedicationDirection = MedicationToAdd.MedicationDirection;
                newMedicine.MedicationQuantity = MedicationToAdd.MedicationQuantity;
                newMedicine.RefillQuantity = MedicationToAdd.RefillQuantity;
                newMedicine.StartDate = MedicationToAdd.StartDate;
                newMedicine.EndDate = MedicationToAdd.EndDate;
                newMedicine.MedicationReason = MedicationToAdd.MedicationReason;
                newMedicine.DoctorName = MedicationToAdd.DoctorName;
                newMedicine.DoctorContact = MedicationToAdd.DoctorContact;
                newMedicine.PharmacyName = MedicationToAdd.PharmacyName;
                newMedicine.PharmacyLocation = MedicationToAdd.PharmacyLocation;
                newMedicine.PharmacyContact = MedicationToAdd.PharmacyContact;
                newMedicine.SideEffects = MedicationToAdd.SideEffects;
                newMedicine.Notes = MedicationToAdd.Notes;
                newMedicine.MedsLeft = MedicationToAdd.MedsLeft;
                newMedicine.MedsLeftReminder = MedicationToAdd.MedsLeftReminder;
                newMedicine.Deleted = MedicationToAdd.Deleted;

                _context.Add(newMedicine);

                // This saves to our database and returns the number of entries that were written to the database
                // _context.SaveChanges();
                result = _context.SaveChanges() != 0;
            

            return result;
            //Else throw a false
        }
    }
}




// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Security.Cryptography;
// using pillpalbackend.Models;
// using pillpalbackend.Models.DTO;
// using pillpalbackend.Services.Context;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using System.Text;
// using System.Security.Claims;
// namespace pillpalbackend.Services
// {
//     public class PillPalService : ControllerBase
//     {
//         private const string BaseUrl = "https://api.fda.gov/drug/ndc.json?search=";
//         public async MedicationDTO SaveMedication(string? MedicationName)
//         {

//             using (var client = new HttpClient())
//             {

//             var response = await client.GetAsync(BaseUrl + MedicationName);
//             if (response.IsSuccessStatusCode){

//             return response;
//             }else return null;
//             }
//         }
//     }
// }