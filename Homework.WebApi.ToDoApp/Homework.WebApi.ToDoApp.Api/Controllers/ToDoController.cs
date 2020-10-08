using Homework.WebApi.ToDoApp.DataModels;
using Homework.WebApi.ToDoApp.Models;
using Homework.WebApi.ToDoApp.Services.Exceptions;
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
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet("get-toDos/{userId}")]
        public ActionResult<List<ToDoDto>> GetToDos(int userId)
        {
            try
            {
                var response = _toDoService.GetUserToDos(userId);
                return Ok(response);
            }
            catch(ToDoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-todo/{userId}")]
        public IActionResult Add([FromBody]ToDoDto toDoRequest, int userId)
        {
            try
            {
                _toDoService.AddToDo(toDoRequest, userId);
                return Ok("To do added!");
            }
            catch(ToDoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-todo/{toDoId}/{userId}")]
        public ActionResult<ToDoDto> GetToDo(int toDoId, int userId)
        {
            try
            {
                var response = _toDoService.GetToDo(toDoId, userId);
                return Ok(response);
            }
            catch (ToDoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-todo/{toDoId}/{userId}")]
        public IActionResult Delete(int toDoId, int userId)
        {
            try
            {
                _toDoService.DeleteToDo(toDoId, userId);
                return Ok("ToDo deleted");
            }
            catch(ToDoException ex)
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
