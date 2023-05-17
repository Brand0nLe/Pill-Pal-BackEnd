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
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public bool DoesUserExist(string? Username)
        {
            // check the table to see if the username exists
            // if 1 item matches the condition, we will return the item
            // if no item matches the condition, it will return null
            // if multiple items match, an error will occur

            return _context.UserInfo.SingleOrDefault(user => user.Username == Username) != null;

            // object != null, true
            // null != null, false
        }

        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            //If the user already exists
            //If they do not exist, we will create the account
            bool result = false;
            if (!DoesUserExist(UserToAdd.Username))
            {
                //if the user does not exist
                // creating a new instance of user model (empty object)
                UserModel newUser = new UserModel();
                // create our salt and hash password
                var hashPassword = HashPassword(UserToAdd.Password);
                newUser.Id = UserToAdd.Id;
                newUser.Firstname = UserToAdd.Firstname;
                newUser.Lastname = UserToAdd.Lastname;
                newUser.Username = UserToAdd.Username;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;

                _context.Add(newUser);

                // This saves to our database and returns the number of entries that were written to the database
                // _context.SaveChanges();
                result = _context.SaveChanges() != 0;
            }

            return result;
            //Else throw a false
        }


        public PasswordDTO HashPassword(string? password)
        {
            PasswordDTO newHashedPassword = new PasswordDTO();

            // this is a byte array with a length of 64
            byte[] SaltByte = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            // enhance rng of numbers without using zero
            provider.GetNonZeroBytes(SaltByte);
            // encoding the 64 digits to string
            // salt makes the hash unique to the user
            // if we only have a hash password, same passwords would match hashes (not good) 
            var Salt = Convert.ToBase64String(SaltByte);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltByte, 10000);

            //encoding our password with out salt
            // if anyone would brute force this, it would take a decade (without a quantum computer)
            var Hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = Hash;

            return newHashedPassword;
        }

        public bool VerifyUserPassword(string? Password, string? storedHash, string? storedSalt)
        {
            // get our existing Salt and change it to base64 string
            var SaltBytes = Convert.FromBase64String(storedSalt);
            // making the password that the user inputed and using the stored salt
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password, SaltBytes, 10000);
            // created the new hash
            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            // checking and returning if the new hash is the same as the old hash
            return newHash == storedHash;
        }

        public IActionResult Login(LoginDTO User)
        {   //want to return an error code if the user does not have a valid username or password
            IActionResult Result = Unauthorized();


            //check to see if the user exists
            if (DoesUserExist(User.Username))
            {
                //true
                //we want to store the user object
                // To create another helper function 
                UserModel foundUser = GetUserByUsername(User.Username);
                //check if the password is correct
                if (VerifyUserPassword(User.Password, foundUser.Hash, foundUser.Salt))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                     ("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
        {
            new Claim("userId", foundUser.Id.ToString())
        };

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken
                    (tokeOptions);
                    Result = Ok(new { Token = tokenString, userId = foundUser.Id, userFname = foundUser.Firstname, userLname = foundUser.Lastname });
                }
            }

            return Result;
        }

        public UserModel GetUserByUsername(string? username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }

        public int GetIdByUsername(string? username)
        {
            var user = _context.UserInfo.SingleOrDefault(user => user.Username == username);
            if (user == null)
            {
                return 0;
            }
            else return user.Id;
        }

        public bool UpdateUser(UserModel userToUpdate)
        {
            //This is sending over the whole object to be updated
            _context.Update<UserModel>(userToUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateUsername(int id, string username)
        {
            //This is sending over just the id and username
            //We have to get the object to then be updated
            UserModel foundUser = GetUserById(id);
            bool result = false;
            if (foundUser != null)
            {
                foundUser.Username = username;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public UserModel GetUserById(int id)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Id == id);
        }

        public int HighestId()
        {
            int highestId = _context.UserInfo.Max(user => user.Id);
            return highestId;
        }

        public bool DeleteUser(string userToDelete)
        {
            //this is just sending over the username
            //we have to get the object to be deleted
            UserModel foundUser = GetUserByUsername(userToDelete);
            bool result = false;
            if (foundUser != null)
            {
                //a user was found
                _context.Remove<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }
    }
}