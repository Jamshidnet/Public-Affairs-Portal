﻿namespace Application.Common.Abstraction;

public interface ICurrentUserService
{
    string Username { get; }
    string Language { get; }
}
