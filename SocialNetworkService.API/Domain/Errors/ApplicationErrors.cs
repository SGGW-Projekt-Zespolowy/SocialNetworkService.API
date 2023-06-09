﻿
using Domain.Shared;

namespace Domain.Errors
{
    public static class ApplicationErrors
    {
        public static class User
        {
            public static Error UserNotFound(Guid userId) => new Error("User.NotFound", $"User with id {userId} was not found.");
            public static Error UserNameNotFound(string fullName) => new Error("User.NotFound", $"User with name: {fullName} was not found.");
            public static readonly Error EmptyRequest = new Error("Request.Empty", "Your request is empty");
        }
    }
}