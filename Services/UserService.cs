using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using pillpalbackend.Models;
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

        public bool DoesUserExist(string? Username)
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
            bool result = false;
            if(!DoesUserExist(UserToAdd.Username))
            {
                //if the user does not exist
                // creating a new instance of user model (empty object)
                UserModel newUser = new UserModel();
                // create our salt and hash password
                var hashPassword = HashPassword(UserToAdd.Password); 
                newUser.ID = UserToAdd.ID;
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
    }
}