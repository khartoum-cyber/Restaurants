using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.UpdateUserDetails
{
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
    {
        private readonly ILogger<UpdateUserDetailsCommandHandler> _logger;
        private readonly IUserContext _userContext;
        private readonly IUserStore<Domain.Entities.User> _userStore;

        public UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger, IUserContext userContext, IUserStore<Domain.Entities.User> userStore)
        {
            _logger = logger;
            _userContext = userContext;
            _userStore = userStore;
        }
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating user: {UserId}, with {@Request}",_userContext.GetCurrentUser()!.Id, request);

            var dbUser = await _userStore.FindByIdAsync(_userContext.GetCurrentUser()!.Id, cancellationToken);

            if (dbUser == null)
                throw new NotFoundException(nameof(User), _userContext.GetCurrentUser()!.Id);

            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DateOfBirth;

            await _userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
