using Abp.Application.Services.Dto;

namespace Revit.Authorization.Users.Dto
{
    public interface IGetLoginAttemptsInput: ISortedResultRequest
    {
        string Filter { get; set; }
    }
}