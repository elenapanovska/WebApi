using Homework.WebApi.ToDoApp.Models;
using Homework.WebApi.ToDoApp.Services;
using Homework.WebApi.ToDoApp.Services.Exceptions;
using Homework.WebApi.ToDoApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Homework.WebApi.ToDoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly string _localhost = "http://localhost:56115";

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("logIn")]
        public IActionResult LogInUser([FromBody]LogIn userRequest)
        {
            try
            {
                var user = _userService.LogIn(userRequest.Username, userRequest.Password);
                if(user != null)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("logInSecretKey@123456"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOptions = new JwtSecurityToken(
                        issuer: _localhost,
                        audience: _localhost,
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = tokenString });
                }else
                {
                    return Unauthorized();
                }
            }
            catch(CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //try
            //{
            //    var response = _userService.LogIn(userRequest.Username, userRequest.Password);
            //    return Ok(response);
            //}
            //catch (CustomException ex) 
            //{
            //    return BadRequest(ex.Message);
            //}
            //catch (Exception ex)
            //{

            //    return BadRequest(ex.Message);
            //}
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterUser userRequest)
        {
            try
            {
                _userService.Register(userRequest);
                return Ok("User registed");
            }
            catch (CustomException ex)
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
