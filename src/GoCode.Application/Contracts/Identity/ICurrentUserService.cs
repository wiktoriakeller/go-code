﻿namespace GoCode.Application.Contracts.Identity
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}