using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskApi.DTOs;
using TaskApi.Entity;
using TaskApi.Services;

namespace TaskApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskServices _taskService;
    public TaskController(ITaskServices taskServices)
    {
        _taskService = taskServices;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllTask()
    {
        var task = await _taskService.GetAllTaskAsync();
        return Ok(task);
    }
    [HttpGet("current")]
    public async Task<IActionResult> GetAllCurrentTask()
    {
        var task = await _taskService.GetAllCurrentTaskAsync();
        return Ok(task);
    }
    [HttpGet("future")]
    public async Task<IActionResult> GetAllFutureTask()
    {
        var task = await _taskService.GetAllFutureTaskAsync();
        return Ok(task);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _taskService.GetByIdAsync(id);
        return Ok(task);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskDto task)
    {
        var createTask = await _taskService.CreateAsync(task);
        if (createTask == null)
        {
            return NotFound();
        }
        return Ok(createTask);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TaskUpdateDto updatetask)
    {
        var task = await _taskService.UpdateAsync(updatetask);
        if (task! == null)
        {
            return NotFound();
        }
        return Ok(task);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var taskId = await _taskService.DeleteAsync(id);
        if (taskId == Guid.Empty)
        {
            return NotFound();
        }
        return NoContent();
    }
    //[HttpPatch]
    //public async Task<IActionResult> InsertOrUpdate(TaskU updatetask)
    //{
    //    var task = await _taskService.SaveAsync(updatetask);
    //    if (task.Id == Guid.Empty)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(task);
    //}
}


