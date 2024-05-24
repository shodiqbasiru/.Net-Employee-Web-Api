using EmployeeApi.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;
using EmployeeApi.Repositories;

namespace EmployeeApi.Services.impls;

public class GroupService : IGroupService
{

    private readonly IRepository<Group> _repository;
    private readonly IPersistence _persistence;

    public GroupService(IRepository<Group> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<Group> CreateGroup(GroupRequest request)
    {
        var payload = new Group
        {
            GroupName = request.GroupName,
            GroupDescription = request.GroupDescription,
        };
        var group = await _repository.SaveAsync(payload);
        await _persistence.SaveChangesAsync();
        return group;
    }

    public async Task<Group> GetById(string id)
    {
        var group = await _repository.FindByIdAsync(Guid.Parse(id));
        if (group is null) throw new Exception("group not found");

        return group;
    }

    public async Task<GroupResponse> GetGroupById(string id)
    {
        var group = await _repository.FindAsync(grp => grp.Id.Equals(Guid.Parse(id)), new [] {"Employees"});
        if (group is null) throw new Exception("group not found");
        
        return GetGroupResponse(group);
        
    }
    
    public async Task<IEnumerable<GroupResponse>> GetAllGroups()
    {
        return (await _repository.FindAllAsync(new [] {"Employees"})).Select(GetGroupResponse);
    }

    public async Task<Group> UpdateGroup(UpdateGroupRequest request)
    {
        var group = await GetById(request.Id);
        group.GroupName = request.GroupName;
        group.GroupDescription = request.GroupDescription;
        var updatedGroup = _repository.Update(group);
        await _persistence.SaveChangesAsync();
        return updatedGroup;
    }

    private static GroupResponse GetGroupResponse(Group group)
    {
        return new GroupResponse
        {
            Id = group.Id.ToString(),
            GroupName = group.GroupName,
            GroupDescription = group.GroupDescription,
            EmployeeResponses = group.Employees?.Select(employee => new EmployeeResponse
            {
                Id = employee.Id,
                Username = employee.Username,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                BirthDate = employee.BirthDate.ToString("yyyy MMMM dd"),
                BasicSalary = employee.BasicSalary,
                IsActive = employee.IsActive,
                CreatedAt = employee.CreatedAt,
            }).ToList()
        };
    }

}