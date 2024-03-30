using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;

internal class MinimumAgeRequrementsHandler(ILogger<MinimumAgeRequrementsHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeReqirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeReqirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Handling MinimumAgeRequirement: User email {Email}, date of birth: {DoB}",
            currentUser.Email,
            currentUser.DateOfBirth);
        
        if(currentUser.DateOfBirth == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if(currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Handing MinimumAgeRequirement: Authorization succeeded!");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        return Task.CompletedTask;

    }
}
