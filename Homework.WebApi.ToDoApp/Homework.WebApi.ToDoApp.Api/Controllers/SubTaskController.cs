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
    public class SubTaskController : ControllerBase
    {
        private readonly ISubTaskService _subTaskService;

        public SubTaskController(ISubTaskService subTaskService)
        {
            _subTaskService = subTaskService;
        }

        [HttpPost("add-subtask")]
        public IActionResult Add(SubTaskDto request)
        {
            try
            {
                _subTaskService.AddSubTask(request);
                return Ok("Success");
            }
            catch(CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-subtask/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _subTaskService.DeleteSubTask(id);
                return Ok("Success");
            }
            catch(CustomException ex)
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
