﻿using Application.Abstractions.Messaging;

namespace Application.Posts.Commands.UpdatePost
{
    public record class UpdatePostCommand(
        Guid postId, string content, string type, string title, bool? caseResolved) : ICommand;
}
