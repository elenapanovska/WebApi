using AutoMapper;
using Homework.WebApi.ToDoApp.DataAccess;
using Homework.WebApi.ToDoApp.DataModels;
using Homework.WebApi.ToDoApp.Models;
using Homework.WebApi.ToDoApp.Services.Exceptions;
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
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public LoggedInUser LogIn(string username, string password)
        {
            var user = _userRepository.GetAll()
                                      .FirstOrDefault(u => u.Username == username);

            if(user == null)
            {
                throw new CustomException(null, null, $"User with username: {username} was not found!");
            }

            var hashedPassword = HashPassword(password);
            if(user.Password != hashedPassword)
            {
                throw new CustomException(user.Id, user.Password, "Password is incorrect!");
            }

            var loggedInUser = _mapper.Map<LoggedInUser>(user);

            return loggedInUser;
        }

        public void Register(RegisterUser userRequest)
        {
            if (string.IsNullOrWhiteSpace(userRequest.FirstName))
            {
                throw new CustomException(null, userRequest.FirstName, "Fist name is required!");
            }

            if (string.IsNullOrWhiteSpace(userRequest.LastName))
            {
                throw new CustomException(null, userRequest.LastName, "Last name is required!");
            }

            if (string.IsNullOrWhiteSpace(userRequest.Username))
            {
                throw new CustomException(null, userRequest.LastName, "Username is required!");
            }

            if(string.IsNullOrWhiteSpace(userRequest.Password) 
                || string.IsNullOrWhiteSpace(userRequest.ConfirmPassword))
            {
                throw new CustomException(null, null, "Password is requried!");
            }

            if(userRequest.Password != userRequest.ConfirmPassword)
            {
                throw new CustomException(null, userRequest.Password, "Passwords do not match!");
            }

            var password = HashPassword(userRequest.Password);


            var user = _mapper.Map<User>(userRequest);
            user.Password = password;

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
