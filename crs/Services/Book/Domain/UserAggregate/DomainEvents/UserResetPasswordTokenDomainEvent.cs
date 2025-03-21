﻿using Domain.Core.Events.Interfaces;
using Domain.UserAggregate.Ids;

namespace Domain.UserAggregate.DomainEvents;

public sealed record UserResetPasswordTokenDomainEvent(
    Guid Id,
    UserId UserId,
    string ReturnUrl)
    : IDomainEvent;
