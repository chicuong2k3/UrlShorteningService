﻿namespace ShorteningService.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}