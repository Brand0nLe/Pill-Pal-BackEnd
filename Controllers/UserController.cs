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

    [HttpGet]
    [Route("HighestId")]
    public int HighestId()
    {
        return _data.HighestId();
    }


    [HttpGet]
    [Route("IdSearch/{id}")]
    public object IdSearch(int id)
    {
        return _data.GetUserById(id);
    }

    [HttpGet]
    [Route("UserSearch/{username}")]
    public object Username(string username)
    {
        return _data.GetUserByUsername(username);
    }

    [HttpGet]
    [Route("IdUserSearch/{username}")]
    public int GetIdByUsername(string username)
    {
        return _data.GetIdByUsername(username);
    }

    //Add a user
    [HttpPost]
    [Route("AddUser")]
    public bool AddUser(CreateAccountDTO UserToAdd)
    {
        return _data.AddUser(UserToAdd);
    }


    [HttpPost]
    [Route("UpdateUser/{id}/{username}")]
    public bool UpdateUser(int id, string username)
    {
        return _data.UpdateUsername(id, username);
    }


    //Delete User Account
    [HttpPost]
    [Route("DeleteUser/{userToDelete}")]
        public bool DeleteUser(string userToDelete)
        {
            return _data.DeleteUser(userToDelete);
        }
}
}