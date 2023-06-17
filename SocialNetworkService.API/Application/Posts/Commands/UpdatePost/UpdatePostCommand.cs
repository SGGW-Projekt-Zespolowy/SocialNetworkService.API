﻿using Application.Abstractions.Messaging;
using Domain.ValueObjects;

namespace Application.Posts.Commands.UpdatePost
{
    public record class UpdatePostCommand(
        Guid postId, string content, MedicalSpecialization type, Title title) : ICommand;
}