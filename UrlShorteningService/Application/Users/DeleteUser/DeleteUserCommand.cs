﻿namespace ShorteningService.Application.Users.DeleteUser;

public record DeleteUserCommand(Guid UserId) : ICommand;
