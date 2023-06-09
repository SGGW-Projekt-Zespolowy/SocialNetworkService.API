﻿using Application.Abstractions.Messaging;

namespace Application.Comments.Commands.CreateComment
{
    public sealed record CreateCommentCommand(Guid authorId, string content, Guid parentPostId, 
        Guid parentCommentId, bool relatedToComment) : ICommand;
}
