using System.Security.Claims;
using CrudTestMicroservice.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CrudTestMicroservice.WebApi.Common.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
