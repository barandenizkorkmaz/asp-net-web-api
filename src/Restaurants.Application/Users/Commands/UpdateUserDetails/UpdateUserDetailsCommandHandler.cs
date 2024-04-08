using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,
    IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser(); // current user
        
        logger.LogInformation("Updating user with id {UserId} with {@Request}", user!.Id, request);
        
        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken); // user stored in DB
        
        if(dbUser == null) throw new NotFoundException(nameof(user), user!.Id);
        
        dbUser.Nationality = request.Nationality;
        
        dbUser.DateOfBirth = request.DateOfBirth;
        
        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
