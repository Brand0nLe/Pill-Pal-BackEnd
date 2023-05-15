using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pillpalbackend.Models;
using pillpalbackend.Models.DTO;
using pillpalbackend.Services;

namespace pillpalbackend.Controllers
{
[ApiController]
[Route("[controller]")]
public class PillPalController : ControllerBase
{

    private readonly PillPalService _data;
    public PillPalController(PillPalService dataFromService)
    {
        _data = dataFromService;
    }
    [HttpGet]
    [Route ("PillPal")]
    public bool Testing()
    {
        return true;
    }

    [HttpPost]
    [Route("AddMedication")]
    public bool AddMedication(MedicationDTO MedicationToAdd)
    {
        return _data.AddMedication(MedicationToAdd);
    }

    [HttpGet]
    [Route("GetMedication/{UserId}")]
    public object ListMeds(int UserId)
    {
        return _data.GetMedicationByUserId(UserId);
    }

}
}