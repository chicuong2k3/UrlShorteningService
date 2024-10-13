using System.Transactions;

namespace ShorteningService.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler(
        UserManager<AppUser> userManager)
        : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
    {
        public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {

            var user = new AppUser
            {
                UserName = command.Email,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber
            };

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var result = await userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                return Result.Error(new ErrorList(result.Errors.Select(err => err.Description)));
            }

            result = await userManager.AddToRoleAsync(user, "customer");

            if (!result.Succeeded)
            {
                return Result.Error(new ErrorList(result.Errors.Select(err => err.Description)));
            }


            transaction.Complete();

            return Result.Created(new RegisterUserResponse(user.Id, user.Email, user.PhoneNumber));
        }
    }
}
