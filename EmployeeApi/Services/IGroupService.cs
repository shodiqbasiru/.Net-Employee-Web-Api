using EmployeeApi.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;

namespace EmployeeApi.Services;

public interface IGroupService
{
    Task<Group> CreateGroup(GroupRequest request);
    Task<Group> GetById(string id);
    Task<GroupResponse> GetGroupById(string id);
    Task<IEnumerable<GroupResponse>> GetAllGroups();
    Task<Group> UpdateGroup(UpdateGroupRequest request);
}