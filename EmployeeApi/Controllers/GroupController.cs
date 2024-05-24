using System.Net;
using EmployeeApi.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers;

[ApiController]
[Route("api/groups")]
public class GroupController : ControllerBase
{

    private readonly IGroupService _service;

    public GroupController(IGroupService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] GroupRequest request)
    {
        var group = await _service.CreateGroup(request);
        var response = new ApiResponse<Group>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Group created successfully",
            Data = group
        };

        return Created($"/api/groups", response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGroupById(string id)
    {
        var group = await _service.GetGroupById(id);
        var response = new ApiResponse<GroupResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Group found successfully",
            Data = group
        };

        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllGroups()
    {
        var groups = await _service.GetAllGroups();
        var response = new ApiResponse<IEnumerable<GroupResponse>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Groups found successfully",
            Data = groups
        };

        return Ok(response);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupRequest request)
    {
        var group = await _service.UpdateGroup(request);
        var response = new ApiResponse<Group>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Group updated successfully",
            Data = group
        };

        return Ok(response);
    }
}