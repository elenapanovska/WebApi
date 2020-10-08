using Homework.WebApi.ToDoApp.DataAccess;
using Homework.WebApi.ToDoApp.DataModels;
using Homework.WebApi.ToDoApp.Models;
using Homework.WebApi.ToDoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Homework.WebApi.ToDoApp.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public LoggedInUser LogIn(string username, string password)
        {
            var user = _userRepository.GetAll()
                                      .FirstOrDefault(u => u.Username == username);

            if(user == null)
            {
                throw new UserException(null, null, $"User with username: {username} was not found!");
            }

            var hashedPassword = HashPassword(password);
            if(user.Password != hashedPassword)
            {
                throw new UserException(user.Id, user.Password, "Password is incorrect!");
            }

            var logInUser = new LoggedInUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username
            };

            return logInUser;
        }

        public void Register(RegisterUser userRequest)
        {
            if (string.IsNullOrWhiteSpace(userRequest.FirstName))
            {
                throw new UserException(null, userRequest.FirstName, "Fist name is required!");
            }

            if (string.IsNullOrWhiteSpace(userRequest.LastName))
            {
                throw new UserException(null, userRequest.LastName, "Last name is required!");
            }

            if (string.IsNullOrWhiteSpace(userRequest.Username))
            {
                throw new UserException(null, userRequest.LastName, "Username is required!");
            }

            if(string.IsNullOrWhiteSpace(userRequest.Password) 
                || string.IsNullOrWhiteSpace(userRequest.ConfirmPassword))
            {
                throw new UserException(null, null, "Password is requried!");
            }

            if(userRequest.Password != userRequest.ConfirmPassword)
            {
                throw new UserException(null, userRequest.Password, "Passwords do not match!");
            }

            var password = HashPassword(userRequest.Password);

            var user = new User
            {
                Id = userRequest.Id,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Username = userRequest.Username,
                Password = password
            };

            _userRepository.Insert(user);
        }

        private string HashPassword(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hashedData = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Encoding.ASCII.GetString(hashedData);
        }
    }
}
