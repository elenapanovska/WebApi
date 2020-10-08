using Homework.WebApi.ToDoApp.Models;
using Homework.WebApi.ToDoApp.Services;
using Homework.WebApi.ToDoApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework.WebApi.ToDoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("logIn")]
        public IActionResult LogInUser([FromBody]LogIn userRequest)
        {
            try
            {
                var response = _userService.LogIn(userRequest.Username, userRequest.Password);
                return Ok(response);
            }
            catch (UserException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterUser userRequest)
        {
            try
            {
                _userService.Register(userRequest);
                return Ok("User registed");
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
