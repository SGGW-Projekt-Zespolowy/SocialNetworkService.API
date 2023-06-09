﻿using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Publications.Commands.DeletePublication
{
    public class DeletePublicationByIdCommandHandler : ICommandHandler<DeletePublicationByIdCommand>
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePublicationByIdCommandHandler(IPublicationRepository publicationRepository, IUnitOfWork unitOfWork)
        {
            _publicationRepository = publicationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeletePublicationByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.publicationId == Guid.Empty) 
                return Result.Failure(Domain.Errors.ApplicationErrors.Request.EmptyRequest);

            var publication = await _publicationRepository.GetByIdAsync(request.publicationId, cancellationToken);

            if (publication is not null)
                _publicationRepository.Remove(publication, cancellationToken);
            else
                return Result.Failure(Domain.Errors.ApplicationErrors.Publication.PublicationNotFound(request.publicationId));

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
