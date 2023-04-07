using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pillpalbackend.Models.DTO;
using pillpalbackend.Services;

namespace pillpalbackend.Controllers
{
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly UserService _data;
    public UserController(UserService dataFromService)
    {
        _data = dataFromService;
    }
    //Login
    [HttpPost]
    [Route("Login")]
    public IActionResult Login([FromBody]LoginDTO User)
    {
        return _data.Login(User);
    }



    [HttpGet]
    [Route("Test")]
    public bool Testing()
    {
        return true;
    }
    //Add a user
    [HttpPost]
    [Route("AddUser")]
    public bool AddUser(CreateAccountDTO UserToAdd)
    {
        return _data.AddUser(UserToAdd);
    }
    //Update User Account

    //Delete User Account
}
}