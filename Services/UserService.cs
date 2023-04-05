using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pillpalbackend.Models.DTO;
using pillpalbackend.Services.Context;

namespace pillpalbackend.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public bool DoesUserExist(string Username)
        {
            // check the table to see if the username exists
            // if 1 item matches the condition, we will return the item
            // if no item matches the condition, it will return null
            // if multiple items match, an error will occur

             return _context.UserInfo.SingleOrDefault(user => user.Username == Username ) != null;

                // object != null, true
                // null != null, false
        } 

        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            //If the user already exists
            //If they do not exist, we will create the account
            //Else throw a false

        }

    }
}